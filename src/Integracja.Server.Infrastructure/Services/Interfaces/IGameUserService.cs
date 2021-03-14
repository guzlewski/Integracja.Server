using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameUserService
    {
        Task<DetailGameUserDto> Get(int gameId, int userId);
        Task<IEnumerable<GameUserDto>> GetActive(int userId);
        Task<IEnumerable<GameUserDto>> GetArchived(int userId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
    }
}
