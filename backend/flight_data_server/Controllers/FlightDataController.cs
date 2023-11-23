using flight_data_server.Database;
using flight_data_server.Interface;
using flight_data_server.Models;
using flight_data_server.Models.FlightData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Serilog;
using System.Text.Json;
using flight_data_server.Migrations.FlightDataDB;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Data.Entity;

namespace flight_data_server.Controllers
    {
    [Route("api/FlightData")]
    [ApiController]
    public class FlightDataController : ControllerBase
        {

        protected APIResponse _response;
        private readonly IFlightDataDBFunctions _dbFlightData;
        private readonly FlightDataDBContext _dbFlightDataCtx;
        private readonly IFlightDataInfoFunctions _dbFlightInfo;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private IHttpContextAccessor _context;


        public FlightDataController(IFlightDataDBFunctions dbFlightData, IFlightDataInfoFunctions dbFlightInfo, FlightDataDBContext dbFlightDataCtx, ILogger<FlightData> logger,  IHttpContextAccessor _context)
            {
            this._dbFlightData = dbFlightData;
            this._response = new();
            this._dbFlightDataCtx = dbFlightDataCtx;
            this._logger = logger;
            this._dbFlightInfo = dbFlightInfo;
            this._context = _context;
            }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<FlightData>> GetFlightData()
            {
            try
                {
                _logger.LogInformation("Get Flight Data Request");

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }

                IEnumerable<FlightData> flightDataList = await _dbFlightData.GetAllAsync();
                _response.Result = flightDataList;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
                }
            catch
                {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
                }
            }

        [HttpGet("getFlightList")]
        public async Task<ActionResult<FlightInfo>> GetFlightList([FromHeader] int pilotID)
            {

            try
                {

                _logger.LogInformation("Get flight list");

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!( isAdmin))
                    {
                    return Unauthorized();
                    }

                IEnumerable<FlightInfo> flightList = await _dbFlightInfo.GetAllAsync(d => d.PilotID == pilotID);
                _response.Result = flightList;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
                }
            catch (Exception e)
                {
                _logger.LogInformation("Exception occured at GetFlightList" + e.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(); // returns a 400 Bad Request response if an exception occurs
                }
            }


        [HttpGet("filter")]
        public async Task<ActionResult<dynamic>> GetFlightDataFilter([FromHeader] int pilotID, [FromHeader] string flightCode)
            {
            try

                {
                _logger.LogInformation("Get filtered flight data");

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }

                var result = await _dbFlightDataCtx.FlightData.Join(
                    _dbFlightDataCtx.FlightInfo, fd => fd.FlightCode, fi => fi.FlightCode, (fd, fi) => new
                        {
                        Latitude = fd.Latitude,
                        Longitude = fd.Longitude,
                        Altitude = fd.Altitude,
                        TrueSpeed = fd.TrueAirSpeed,
                        PilotID = fi.PilotID,
                        FlightCode = fi.FlightCode,
                        LoggingTime = fd.LoggingTime

                        }
                    ).Where(d => d.PilotID == pilotID && d.FlightCode == flightCode).ToListAsync();

                return Ok(result); // returns a 200 OK response with the JSON as the content
                }
            catch (Exception e)
                {
                _logger.LogInformation("Exception occured at GetFlight" + e.ToString());
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(); // returns a 400 Bad Request response if an exception occurs
                }
            }

        [HttpGet("get/latest")]

        public async Task<ActionResult<FlightData>> GetLatestData([FromHeader] String flightCode)
            {

            try
                {
                _logger.LogInformation("Get Latest Data Request");

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }

                FlightData lastData = _dbFlightDataCtx.FlightData.OrderByDescending(x => x.LoggingTime).FirstOrDefault(p => p.FlightCode == flightCode);

                if (lastData == null)
                    {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return BadRequest(_response);
                    }
                else
                    {
                    _response.Result = lastData;
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(_response);
                    }
                }


            catch
                {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
                }
            }


        [HttpPost]
        public async Task<ActionResult<FlightData>> PostFlightData([FromBody] FlightData flighData)
            {

            try
                {

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }

                _logger.LogInformation("Post Data " + flighData.ToString());
                if (flighData == null)
                    {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    }
                await _dbFlightData.CreateAsync(flighData);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;

                return Ok(_response);
                }
            catch (Exception e)
                {
                _logger.LogInformation("Error message " + e.ToString());
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
                }

            }

        [HttpPost("startFlightCycle")]
        public async Task<ActionResult<FlightInfo>> StartFlightCycle([FromBody] FlightInfo flightInfo)
            {

            try
                {
                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }

                _logger.LogInformation("Flight Cycle Started with data " + flightInfo.ToString());
                if (flightInfo == null)
                    {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    }

                await _dbFlightInfo.CreateAsync(flightInfo);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
                }
            catch (Exception e)
                {
                _logger.LogInformation("Error message " + nameof(StartFlightCycle) + e.ToString());
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
                }

            }
        [HttpPost("endFlightCycle")]
        public async Task<ActionResult<FlightInfo>> EndFlightCycle()
            {

            try
                {

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }

                _logger.LogInformation("Flight Cycle ended with data ");

                int pilotID = 5;
                var flights = await _dbFlightDataCtx.FlightInfo.Where(fi => fi.PilotID == pilotID).ToListAsync();

                foreach (var flight in flights)
                    {
                    flight.isActive = false;
                    }
                _dbFlightDataCtx.SaveChangesAsync();

                return Ok();

                }
            catch (Exception e)
                {
                _logger.LogInformation("Error message " + nameof(EndFlightCycle) + e.ToString());
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
                }

            }

        private bool IsAdmin(ClaimsPrincipal principal)
            {
            // Implement the logic to check if the user has the "admin" role
            // For simplicity, let's assume the role is stored in the "role" claim.
            return principal?.IsInRole("admin") ?? false;
            }

        private bool IsUser(ClaimsPrincipal principal)
            {
            // Implement the logic to check if the user has the "admin" role
            // For simplicity, let's assume the role is stored in the "role" claim.
            return principal?.IsInRole("user") ?? false;
            }
        }

    }
