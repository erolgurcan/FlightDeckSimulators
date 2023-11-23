using flight_data_server.Database;
using flight_data_server.Interface;
using flight_data_server.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace flight_data_server.Services.UserFunctions
    {
    public class UserRepository : IUserRepository
        {
        private readonly UserDBContext _db;
        private readonly string _secretKey;
        private string secretKey;


        public UserRepository(UserDBContext db, IConfiguration configuration)
            {
            this._db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");

            }

        public bool IsUniqueUser(string username)
            {
            try
                {
                var user = _db.UserData.FirstOrDefault(u => u.UserName == username);

                return user == null ? true : false;
                }
            catch (Exception)
                {
                throw new NotImplementedException();
                }
            }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
            {

            var user = await _db.UserData.FirstOrDefaultAsync(
                u =>
                    u.UserName.ToLower() == loginRequest.UserName.ToLower()
            );

            if (user?.UserName == null)
                {
                return new LoginResponse()
                    {
                    Token = "",
                    User = null
                    };
                }

            if (!BCryptNet.Verify(loginRequest.Password, user?.Password))
                {
                return new LoginResponse()
                    {
                    Token = "",
                    User = null
                    };
                }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
                {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user?.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user?.Role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            LoginResponse loginResponse = new()
                {
                Token = jwt,
                User = user
                };
            return loginResponse;
            }



        public async Task<User> Register(RegisterRequest registerRequest)
            {
            try
                {

                User user = new User()
                    {
                    UserName = registerRequest.UserName,
                    Password = BCryptNet.HashPassword(registerRequest.Password, 10),
                    Role = registerRequest.Role
                    };

                _db.UserData.Add(user);
                _db.SaveChangesAsync();

                user.Password = "";

                return user;
                }
            catch (Exception)
                {
                throw new NotImplementedException();
                }
            }
        }

        
    }
