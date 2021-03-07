using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Integracja.Server.Infrastructure.Jwt;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces.Implementations;
using Microsoft.IdentityModel.Tokens;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public TokenDto GenerateToken(int userId, Guid sessionGuid)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, sessionGuid.ToString()));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var nowDate = DateTime.UtcNow;
            var expireDate = nowDate.AddSeconds(_jwtSettings.TokenExpirationTime);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateEncodedJwt(_jwtSettings.ValidIssuer, _jwtSettings.ValidAudience, claimsIdentity, nowDate, expireDate, nowDate, signingCredentials);

            return new TokenDto()
            {
                ExpiryIn = _jwtSettings.TokenExpirationTime,
                ExpireOnDate = expireDate,
                Token = token
            };
        }
    }
}
