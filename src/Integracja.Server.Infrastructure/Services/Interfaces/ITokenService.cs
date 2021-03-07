using System;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces.Implementations
{
    public interface ITokenService
    {
        TokenDto GenerateToken(int userId, Guid sessionGuid);
    }
}
