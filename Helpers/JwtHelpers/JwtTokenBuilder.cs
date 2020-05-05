using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace DSM.UI.Api.Helpers.JwtHelpers
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey _securityKey;
        private string _subject = "";
        private string _issuer = "";
        private string _audience = "";
        private readonly Dictionary<string, string> _claims = new Dictionary<string, string>();
        private int _expirationValue = 6;
        private ExpirationUnit _expirationUnit = ExpirationUnit.Hours;

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this._securityKey = securityKey;
            return this;
        }

        public JwtTokenBuilder AddSubject(string subject)
        {
            this._subject = subject;
            return this;
        }

        public JwtTokenBuilder AddIssuer(string issuer)
        {
            this._issuer = issuer;
            return this;
        }

        public JwtTokenBuilder AddAudience(string audience)
        {
            this._audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            this._claims.Add(type, value);
            return this;
        }

        public JwtTokenBuilder AddRole(string value)
        {
            this._claims.Add(ClaimTypes.Role, value);
            return this;
        }

        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            _ = this._claims.Union(claims);
            return this;
        }

        public JwtTokenBuilder AddExpireTime(int value, ExpirationUnit expiration)
        {
            this._expirationValue = value;
            this._expirationUnit = expiration;
            return this;
        }

        public JwtToken Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, this._subject),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(this._claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                              issuer: this._issuer,
                              audience: this._audience,
                              claims: claims,
                              expires: DateTime.UtcNow.AddHours(_expirationValue),
                              signingCredentials: new SigningCredentials(
                                                        this._securityKey,
                                                        SecurityAlgorithms.HmacSha256));

            return new JwtToken(token);
        }

        #region " private "

        private void EnsureArguments()
        {
            if (this._securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this._issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this._audience))
                throw new ArgumentNullException("Audience");
        }

        #endregion

        public enum ExpirationUnit
        {
            Days, Hours, Minutes
        }
    }
}
