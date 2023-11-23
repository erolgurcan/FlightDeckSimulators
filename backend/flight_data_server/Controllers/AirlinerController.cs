using flight_data_server.Database;
using flight_data_server.Interface;
using flight_data_server.Models;
using flight_data_server.Models.Airliner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace flight_data_server.Controllers
    {

    [Route("api/Airliner")]
    [ApiController]
    public class AirlinerController : ControllerBase
        {
        private readonly ILogger<AirlinerController> _logger;
        private readonly AirlinerDBContext _adbContext;
        private readonly IAirlinerDBFunctions _dbAirliner;
        protected APIResponse _response;
        private IHttpContextAccessor _context;

        public AirlinerController(ILogger<AirlinerController> logger, AirlinerDBContext adbContext, IAirlinerDBFunctions dbAirliner, IHttpContextAccessor _context)
            {
            this._logger = logger;
            this._adbContext = adbContext;
            this._dbAirliner = dbAirliner;
            this._response = new();
            this._context = _context;


            }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAirliners()
            {
            _logger.LogInformation("Getting All Values");

            var user = _context.HttpContext.User;

            bool isUser = IsUser(user);
            bool isAdmin = IsAdmin(user);

            if( !(isUser || isAdmin))
                {
                return Unauthorized();
                }

            IEnumerable<Airliner> airlinerList = await _dbAirliner.GetAllAsync();
            _response.Result = airlinerList;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            }

        [HttpGet("{id:int}", Name = "GetAirlier")]
        public async Task<ActionResult<Airliner>> GetAirliner(int id, HttpContext context)
            {
            if (id == 0)
                {
                return BadRequest();
                }

            var user = _context.HttpContext.User;

            bool isUser = IsUser(user);
            bool isAdmin = IsAdmin(user);

            if (!(isUser || isAdmin))
                {
                return Unauthorized();
                }

            IEnumerable<Airliner> airlinerList = await _dbAirliner.GetAllAsync();
            _response.Result = airlinerList;
            _response.StatusCode = HttpStatusCode.OK;


            if (airlinerList == null)
                {
                return NotFound();
                }

            return Ok(_response);
            }


        [HttpPost]
        public async Task<ActionResult<Airliner>> CreateAirliner([FromBody] Airliner airliner)
            {

            var user = _context.HttpContext.User;

            bool isUser = IsUser(user);
            bool isAdmin = IsAdmin(user);

            if (!(isAdmin))
                {
                return Unauthorized();
                }

            var airlinerCheck = await _adbContext.Airliner.FirstOrDefaultAsync(
                u => u.AirlinerName.ToLower() == airliner.AirlinerName.ToLower());

            if (airliner == null)
                {
                ModelState.AddModelError("Custom Error", "No Airliner Passed");
                return BadRequest(ModelState);
                }

            if (airlinerCheck != null)
                {
                ModelState.AddModelError("Custom Error", "Airliner Already Exist");
                return BadRequest();
                }


            Airliner newAirliner = new()
                {
                AirlinerName = airliner.AirlinerName,
                AirlinerCallSign = airliner.AirlinerCallSign
                };


            await _dbAirliner.CreateAsync(newAirliner);



            return Ok(airliner);


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
