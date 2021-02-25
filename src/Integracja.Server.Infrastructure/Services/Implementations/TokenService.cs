using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;

        public TokenService(IOptions<JwtSettings> options, UserManager<User> userManager)
        {
            _jwtSettings = options.Value;
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

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var expireDate = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenExpirationTime);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                expires: expireDate,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new TokenDto()
            {
                ExpiryIn = _jwtSettings.TokenExpirationTime,
                ExpireOnDate = expireDate,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
