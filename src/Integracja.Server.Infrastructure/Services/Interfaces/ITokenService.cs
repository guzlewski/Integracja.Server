using System;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        TokenDto GenerateToken(int userId, Guid sessionGuid);
    }
}
