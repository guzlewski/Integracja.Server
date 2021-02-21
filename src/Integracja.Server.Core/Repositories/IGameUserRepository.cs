using System;
using System.Threading.Tasks;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameUserRepository
    {
        Task Accept(int gameId, int userId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
    }
}
