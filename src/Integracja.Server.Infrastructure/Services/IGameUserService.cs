using System;
using System.Threading.Tasks;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGameUserService
    {
        Task Accept(int gameId, int userId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
    }
}
