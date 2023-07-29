using Data.Model;
using Data.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService userService;
        public UserController(IUserService UserService)
        {
            this.userService = UserService;
        }
        [HttpGet]
        public IActionResult GETdata()
        {
            return Ok(userService.GetUser());
        } 

            [HttpPost]
        public IActionResult Index(UserView user)
        {
            var u = userService.Login(user);
            if (u == null)
            {
                return Ok(new ApiResponse { Success = false, Message = "Invalid Username or Password",Data= null });
            }
            else
            {
                //cap token
                string token = GenarateToken(u);
                var random = new byte[32];
                string refreshtoken;
                    using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(random);
                    refreshtoken = Convert.ToBase64String(random);
                }
                    
                return Ok(new ApiResponse { Success = true, Message = "Well come!", Data = new { token= token, refreshtoken= refreshtoken }  });
            }
        }

        private string GenarateToken(User user)
        {
            var jwtTokenHandle = new JwtSecurityTokenHandler();
            var SecretKeyByte = Encoding.UTF8.GetBytes(userService.getAppSettings().Secretkey);
            var tokenDescription = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserName", user.UserName)
                    , new Claim("UserId", user.UserID.ToString()),

                    // roles
                    new Claim("TokenID", Guid.NewGuid().ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyByte),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandle.CreateToken(tokenDescription);
            return jwtTokenHandle.WriteToken(token);
        }
    }
}
