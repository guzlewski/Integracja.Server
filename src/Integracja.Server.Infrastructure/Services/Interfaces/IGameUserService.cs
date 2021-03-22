using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameUserService
    {
        Task<T> Get<T>(int gameId, int userId);
        Task<IEnumerable<T>> GetActive<T>(int userId);
        Task<IEnumerable<T>> GetArchived<T>(int userId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
    }
}
