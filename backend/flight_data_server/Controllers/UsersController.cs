using flight_data_server.Interface;
using flight_data_server.Models;
using flight_data_server.Models.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BCrypt.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using flight_data_server.Database;
using System.Security.Claims;

namespace flight_data_server.Controllers
    {

    [Route("api/UsersAuth")]
    [ApiController]

    public class UsersController : Controller
        {

        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        private readonly ILogger _logger;
        private readonly UserDBContext _userDBCtx;
        private IHttpContextAccessor _context;

        public UsersController(IUserRepository userRepo, ILogger<User> logger, UserDBContext userDBCtx, IHttpContextAccessor _context)
            {
            _userRepo = userRepo;
            this._response = new();
            this._logger = logger;
            this._userDBCtx = userDBCtx;
            this._context = _context;
            }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
            {
            try
                {
                _logger.LogInformation("Login request");

                var user = _context.HttpContext.User;

                bool isUser = IsUser(user);
                bool isAdmin = IsAdmin(user);

                if (!(isUser || isAdmin))
                    {
                    return Unauthorized();
                    }


                var loginResponse = await _userRepo.Login(model);

                if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                    {

                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage.Add("Username or password is incorrect");
                    return Ok(_response);
                    }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponse;
                return Ok(_response);
                }
            catch (Exception e)
                {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
                }

            }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
            {

            var userxtx = _context.HttpContext.User;

            bool isUser = IsUser(userxtx);
            bool isAdmin = IsAdmin(userxtx);

            if (!(isUser || isAdmin))
                {
                return Unauthorized();
                }

            Regex pattern = new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");

            if (!pattern.IsMatch(model.Password))
                {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Password has to be at least eight characters, at least one letter, one number and one special character");
                return BadRequest(_response);
                }

            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
                {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Username already exists");
                return BadRequest(_response);
                }

            var user = await _userRepo.Register(model);
            if (user == null)
                {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Error while registering");
                return BadRequest(_response);
                }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
            }


        [HttpGet("getPilotProfile")]
        public async Task<IActionResult> GetPilotProfile([FromHeader] int userID)
            {

            _logger.LogInformation("Get Pilot Profile");

            if (userID == null)
                {

                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("User ID not specified");
                return BadRequest(_response);
                }

            var result = _userDBCtx.UserData.Join(

                    _userDBCtx.PilotProfile, u => u.UserId, p => p.UserProfileID, (u, p) =>
                    new
                        {
                        PilotId = p.PilotId,
                        PilotRating = p.PilotRating,
                        UserID = u.UserId,
                        PilotName = p.PilotProfileName
                        }
                ).Where(d => d.UserID == userID);


            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = result;
            return Ok(_response);
            }

        [Authorize]
        [HttpGet("authorize")]

        public async Task<IActionResult> Authorize()
            {

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
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
