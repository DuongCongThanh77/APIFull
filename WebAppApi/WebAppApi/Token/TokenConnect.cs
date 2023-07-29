using Data.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAppApi.Token
{
    public class TokenConnect
    {
        public TokenModel CreateToken(string SecretKey,User user)
        {
            var secretkey = Encoding.UTF8.GetBytes(SecretKey);
            var tokenHandle = new JwtSecurityTokenHandler();
            var tokenCreate = tokenHandle.CreateToken(
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { 
                     new Claim("UserID",user.UserID.ToString()),
                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256Signature)
                }
        );  
        
            
            return new TokenModel { accessToken = tokenHandle.WriteToken(tokenCreate) , RefreshToken = "" };
        }

        public TokenModel RenewToken()
        {

            return new TokenModel { accessToken = "", RefreshToken = "" };
        }
    }
}
