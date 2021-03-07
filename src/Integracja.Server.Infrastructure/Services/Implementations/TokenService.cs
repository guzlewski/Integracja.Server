using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Integracja.Server.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
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
            var token = handler.CreateEncodedJwt(_jwtSettings.Issuer, _jwtSettings.Audience, claimsIdentity, nowDate, expireDate, nowDate, signingCredentials);

            return new TokenDto()
            {
                ExpiryIn = _jwtSettings.TokenExpirationTime,
                ExpireOnDate = expireDate,
                Token = token
            };
        }
    }
}
