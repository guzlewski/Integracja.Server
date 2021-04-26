using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameService
    {
        Task<T> Get<T>(int id, int userId, bool skipUserVerification = false);
        Task<IEnumerable<T>> GetAll<T>(int userId, bool skipUserVerification = false);
        Task<IEnumerable<T>> GetEnded<T>(int userId, bool skipUserVerification = false);
        Task<IEnumerable<T>> GetCurrent<T>(int userId, bool skipUserVerification = false);
        Task<int> Add(CreateGameDto createGameDto, int userId, bool skipUserVerification = false);
        Task Delete(int id, int userId, bool skipUserVerification = false);
        Task<int> Update(int id, EditGameDto editGameDto, int userId, bool skipUserVerification = false);
    }
}
