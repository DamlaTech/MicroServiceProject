using APP.Identities.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APP.Identities.Features.Identities
{
    public class TokenRequest : Request, IRequest<TokenResponse>
    {
        [Required, StringLength(30, MinimumLength = 3)]
        public string IdentityName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string Password { get; set; }

        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }
    }

    public class TokenResponse : CommandResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public TokenResponse(bool isSuccessful, string message = "", int id = 0) : base(isSuccessful, message, id)
        {
        }
    }

    public class TokenHandler : IdentityDbHandler, IRequestHandler<TokenRequest, TokenResponse>
    {
        public TokenHandler(IdentitiesDb db) : base(db)
        {
        }

        public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            var identity = await _db.Identities.Include(u => u.Role).SingleOrDefaultAsync(u =>
                u.IdentityName == request.IdentityName && u.Password == request.Password && u.IsActive);
            if (identity is null)
                return new TokenResponse(false, "Active user with the user name and password not found!");

            // refresh token:
            identity.RefreshToken = CreateRefreshToken();
            identity.RefreshTokenExpiration = DateTime.Now.AddDays(AppSettings.RefreshTokenExpirationInDays);
            _db.Identities.Update(identity);
            await _db.SaveChangesAsync(cancellationToken);

            var claims = GetClaims(identity);
            var expiration = DateTime.Now.AddMinutes(AppSettings.ExpirationInMinutes);
            var token = CreateAccessToken(claims, expiration);

            return new TokenResponse(true, "Token created successfully.", identity.Id)
            {
                Token = JwtBearerDefaults.AuthenticationScheme + " " + token,
                RefreshToken = identity.RefreshToken
            };
        }
    }
}
