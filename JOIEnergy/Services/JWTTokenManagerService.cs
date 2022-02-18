using JOIEnergy.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JOIEnergy.Services
{
    public class JWTTokenManagerService : IJWTTokenManagerService
    {
        private byte[] _tokenSecretKey = new byte[0];
        private TokenValidationParameters _tokenValidationParameters = null;

        private Dictionary<string, string> userDict = new Dictionary<string, string>() {
            {"user1", "password1" },
            {"user2", "password2" }
        };

        public JWTTokenManagerService()
        {
            _tokenSecretKey = Encoding.ASCII.GetBytes("MySecretKeyIsVeryStrong#");
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_tokenSecretKey),
                ValidateIssuer = true,
                ValidIssuer = "NiteshGoyal",
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateToken(string user, string pwd)
        {
            var result = "";
            if (userDict.ContainsKey(user))
            {
                // generate token that is valid for 7 days
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user), new Claim("emailId", "myemail@mysite.com") }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = "NiteshGoyal",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_tokenSecretKey), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result = tokenHandler.WriteToken(token);
            }

            return result;
        }

        public UserProfile ValidateToken(string token)
        {
            if (token == null)
                return null;
           
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var payLoad = jwtToken.Payload.ToString();
                // return user id from JWT token if validation successful
                return new UserProfile
                {
                    id = jwtToken.Claims.First(x => x.Type == "id").Value,
                    emailId = jwtToken.Claims.First(x => x.Type == "emailId").Value
                };
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
