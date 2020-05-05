using System;
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

            var userClaim = tokenObj.Claims.ToArray()[0];
            if (userClaim != null)
            {
                decodedToken.Username = userClaim.Value;
            }

            var roleClaim = tokenObj.Claims.ToArray()[1];
            if (roleClaim != null)
            {
                decodedToken.Userrole = roleClaim.Value;
            }
            decodedToken.ValidFrom = tokenObj.ValidFrom;
            decodedToken.ValidTo = tokenObj.ValidTo;
            decodedToken.Issuer = tokenObj.Issuer;
            decodedToken.IssuedAt = tokenObj.IssuedAt;
            return decodedToken;
        }
    }
}
