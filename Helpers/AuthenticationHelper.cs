using DSM.UI.Api.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DSM.UI.Api.Helpers
{
    public static class AuthenticationHelper
    {
        public static string GetToken(User user, string secret)
        {
            Dictionary<string, object> claims = new Dictionary<string, object>
            {
                { ClaimTypes.Name, user.FullName },
                { ClaimTypes.Actor, user.Username },
                { ClaimTypes.Role, user.Role.Name }
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = claims,
                Issuer = "Doğuş Teknoloji",
                Audience = "DT Users",
                IssuedAt = DateTime.UtcNow
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);


            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
