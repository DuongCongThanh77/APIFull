using Data.Model;
using Data.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                var token = GenarateToken(u);
            
                    
                return Ok(new ApiResponse { Success = true, Message = "Well come!", Data = token });
            }
        }

        private TokenModel GenarateToken(User user)
        {
            var jwtTokenHandle = new JwtSecurityTokenHandler();
            var SecretKeyByte = Encoding.UTF8.GetBytes(userService.getAppSettings().Secretkey);
            var tokenDescription = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserName", user.UserName)
                    , new Claim("UserId", user.UserID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                    // roles
                    new Claim("TokenID", Guid.NewGuid().ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyByte),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandle.CreateToken(tokenDescription);
            var tokenResult= jwtTokenHandle.WriteToken(token);

            // RefreshToken
            var random = new byte[32];
            string refreshtoken;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }
            refreshtoken = Convert.ToBase64String(random);

            var DbRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserID = user.UserID,
                IsUsed = false,
                ExpireTime = DateTime.Now.AddHours(1),
                IssuedAt = DateTime.UtcNow,
                Token = refreshtoken,
                JwtId = token.Id,
                IsRevoked = false
            };
            userService.AddReFreshToken(DbRefreshToken);


            var tokenModel = new TokenModel { AccessToken= tokenResult, RefreshToken= refreshtoken };

            return tokenModel; 
        }


        [HttpPost("ReNewToken")]
        public IActionResult RenewToken(TokenModel tokenModel)
        {
            // Check Token to renew 
            var jwtTokenHandle = new JwtSecurityTokenHandler();
            var SecretKeyByte = Encoding.UTF8.GetBytes(userService.getAppSettings().Secretkey);
            try
            {
                // param for ValidateToken
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    // tu cap token
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // ky vao token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKeyByte),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false // does not ckeck expired time
                    
                };

                // 1. Check format accesstoken
                var verifyToken = jwtTokenHandle.ValidateToken(tokenModel.AccessToken, tokenValidationParameters, out var validatedToken);
                
                // 2. check algorithm
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var checkAlg = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!checkAlg) // Alg not mapping
                    {
                        return Ok(new ApiResponse { Success = false, Message = "Invalid Token", Data = null });
                    } 
                }

                // 3. Check expired accesstoken
                var LongexpireDate = long.Parse(verifyToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiredDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(LongexpireDate);
                if(expiredDate > DateTime.UtcNow)
                {
                    return Ok(new ApiResponse { Success = false, Message = "Token has not yet expired" });
                }

                // 4. Check RereshToken exist in DB
                if (!userService.CheckReFreshToken(tokenModel.RefreshToken))
                {
                    return Ok(new ApiResponse { Success = false, Message = "TokenRefresh does not exist" });
                }
                RefreshToken refreshToken = userService.GetReFreshToken(tokenModel.RefreshToken);
                //5. check refresh token isused/revoke ?
                if (refreshToken.IsUsed)
                {
                    return Ok(new ApiResponse { Success = false, Message = "TokenRefresh was used" });
                }
                if(refreshToken.IsRevoked)
                {
                    return Ok(new ApiResponse { Success = false, Message = "TokenRefresh was Revoked" });
                }

                // check ID token


                // renew Token
                refreshToken.IsUsed = true;
                refreshToken.IsRevoked = true;
                userService.UpdateReFreshToken(refreshToken);

                var token = GenarateToken(userService.GetUser(refreshToken.UserID));


                return Ok(new ApiResponse { Success = true, Message = "Refresh Token complete!", Data = new { token = token.AccessToken, refreshtoken = token.RefreshToken } });
            }
            catch
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Something went wrong", Data = null });
            }
        }


    }
}
