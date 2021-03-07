using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public interface IGameService
    {
        Task<DetailGameDto> Get(int id, int userId);
        Task<IEnumerable<GameDto>> GetAll(int userId);
        Task<int> Add(CreateGameDto createGameDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditGameDto editGameDto, int userId);
    }
}
