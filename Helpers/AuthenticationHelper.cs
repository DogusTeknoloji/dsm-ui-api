using DSM.UI.Api.Models.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DSM.UI.Api.Helpers
{
    public static class AuthenticationHelper
    {
        public static string GetToken(GetUserModel user, string secret)
        {
            user.MobilePhone ??= "";
            user.MailAddress ??= "";
            user.SamAccountName ??= "";

            Dictionary<string, object> claims = new Dictionary<string, object>
            {
                { ClaimTypes.Name, user.Username },
                { ClaimTypes.Actor, user.FullName },
                { ClaimTypes.Email, user.MailAddress },
                { ClaimTypes.MobilePhone, user.MobilePhone },
                { ClaimTypes.WindowsAccountName, user.SamAccountName },
                { ClaimTypes.Role, user.Role.Name }
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(ClaimTypes.Actor, user.FullName),
                    new Claim(ClaimTypes.Email,user.MailAddress),
                    new Claim(ClaimTypes.MobilePhone,user.MobilePhone),
                    new Claim(ClaimTypes.WindowsAccountName,user.SamAccountName)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = claims,
                Issuer = "Doğuş Teknoloji",
                Audience = user.Company,
                IssuedAt = DateTime.UtcNow
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
