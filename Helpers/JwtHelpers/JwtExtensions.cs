using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DSM.UI.Api.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        public static DecodedTokenStruct DecodeToken(string token)
        {
            DecodedTokenStruct decodedToken = new DecodedTokenStruct();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenObj = handler.ReadToken(token) as JwtSecurityToken;

            if (tokenObj.Claims.Count() < 2)
            {
                return decodedToken;
            }
            object tempvalue;
            tokenObj.Payload.TryGetValue("unique_name", out tempvalue);
            decodedToken.FullName = tempvalue?.ToString();

            tokenObj.Payload.TryGetValue("role", out tempvalue);
            decodedToken.Userrole = tempvalue?.ToString();

            tokenObj.Payload.TryGetValue("email", out tempvalue);
            decodedToken.Email = tempvalue?.ToString();

            tokenObj.Payload.TryGetValue("winaccountname", out tempvalue);
            decodedToken.Username = tempvalue?.ToString();

            decodedToken.ValidFrom = tokenObj.ValidFrom;
            decodedToken.ValidTo = tokenObj.ValidTo;
            decodedToken.Issuer = tokenObj.Issuer;
            decodedToken.IssuedAt = tokenObj.IssuedAt;
            return decodedToken;
        }
    }
}
