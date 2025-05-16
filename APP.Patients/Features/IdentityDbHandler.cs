using APP.Identities.Domain;
using CORE.APP.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APP.Identities.Features
{
    public class IdentityDbHandler : Handler
    {
        protected readonly IdentitiesDb _db;

        public IdentityDbHandler(IdentitiesDb db) : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        protected virtual List<Claim> GetClaims(Identity identity)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, identity.IdentityName),
                new Claim(ClaimTypes.Role, identity.Role.Name),
                new Claim("Id", identity.Id.ToString())
            };
        }

        protected virtual string CreateAccessToken(List<Claim> claims, DateTime expiration)
        {
            var signingCredentials = new SigningCredentials(AppSettings.SigningKey, SecurityAlgorithms.HmacSha256Signature);
            var jwtSecurityToken = new JwtSecurityToken(AppSettings.Issuer, AppSettings.Audience, claims, DateTime.Now, expiration, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        }

        protected virtual string CreateRefreshToken()
        {
            var bytes = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        protected virtual ClaimsPrincipal GetPrincipal(string accessToken)
        {
            accessToken = accessToken.StartsWith(JwtBearerDefaults.AuthenticationScheme) ?
                accessToken.Remove(0, JwtBearerDefaults.AuthenticationScheme.Length + 1) : accessToken;
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AppSettings.SigningKey
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = jwtSecurityTokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
            return securityToken is null ? null : principal;
        }
    }
}
