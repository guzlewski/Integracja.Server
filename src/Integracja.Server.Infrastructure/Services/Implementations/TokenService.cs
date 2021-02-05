using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IOptionsMonitor<JwtConfig> _options;
        private readonly UserManager<User> _userManager;

        public TokenService(IOptionsMonitor<JwtConfig> options, UserManager<User> userManager)
        {
            _options = options;
            _userManager = userManager;
        }

        public async Task<TokenDto> GenerateToken(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new UnauthorizedException("Invalid username or password.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.CurrentValue.SecretKey));
            var expireDate = DateTime.UtcNow.AddSeconds(_options.CurrentValue.TokenExpirationTime);

            var token = new JwtSecurityToken(
                issuer: _options.CurrentValue.ValidIssuer,
                audience: _options.CurrentValue.ValidAudience,
                expires: expireDate,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new TokenDto()
            {
                ExpiryIn = _options.CurrentValue.TokenExpirationTime,
                ExpireOnDate = expireDate,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
