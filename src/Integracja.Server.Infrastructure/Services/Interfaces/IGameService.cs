using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameService
    {
        Task<T> Get<T>(int id, int userId);
        Task<IEnumerable<T>> GetAll<T>(int userId);
        Task<int> Add(CreateGameDto createGameDto, int userId);
        Task Delete(int id, int userId);
        Task<int> Update(int id, EditGameDto editGameDto, int userId);
    }
}
