using System;
using System.IdentityModel.Tokens.Jwt;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(int userId, Guid sessionGuid);
    }
}
