using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGamemodeService
    {
        Task<GamemodeDto> Get(int id, int userId);
        Task<IEnumerable<GamemodeDto>> GetAll(int userId);
        Task<GamemodeDto> Add(GamemodeDto dto, int userId);
        Task Delete(int id, int userId);
        Task Update(int id, GamemodeDto dto, int userId);
    }
}
