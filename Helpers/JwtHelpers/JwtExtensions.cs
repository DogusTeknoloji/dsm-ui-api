using System;
using DSM.UI.Api.Models.User;
using Microsoft.Extensions.Configuration;

namespace DSM.UI.Api.Helpers.JwtHelpers
{
    public static class JwtExtensions
    {
        public static string GenerateToken(this TokenModel model)
        {
            try
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(model.SecretKey))
                                .AddIssuer(model.Issuer)
                                .AddAudience(model.Audience)
                                .AddExpireTime(6, JwtTokenBuilder.ExpirationUnit.Hours)
                                .AddRole(model.Role)
                                .AddClaims(model.Claims)
                                .Build();
                return token.Value;
                //model.t = token.Value;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }

        }
    }
}
