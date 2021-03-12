using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGamemodeService
    {
        Task<DetailGamemodeDto> Get(int id, int userId);
        Task<IEnumerable<GamemodeDto>> GetAll(int userId);
        Task<IEnumerable<GamemodeDto>> GetOwned(int userId);
        Task<int> Add(CreateGamemodeDto createGamemodeDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditGamemodeDto editGamemodeDto, int userId);
    }
}
