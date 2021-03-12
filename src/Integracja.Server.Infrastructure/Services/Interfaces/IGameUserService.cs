using System;
using System.Threading.Tasks;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameUserService
    {
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
    }
}
