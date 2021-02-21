using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGameUserService
    {
        Task Accept(int gameId, int userId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
        Task<GameUserGet> Get(int gameId, int userId);
        Task<IEnumerable<GameUserGetAll>> GetAll(int userId);
    }
}
