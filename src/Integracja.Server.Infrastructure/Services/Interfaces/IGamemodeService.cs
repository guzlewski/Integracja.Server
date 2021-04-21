using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGamemodeService
    {
        Task<T> Get<T>(int id, int userId, bool skipUserVerification = false);
        Task<IEnumerable<T>> GetAll<T>(int userId, bool skipUserVerification = false);
        Task<IEnumerable<T>> GetOwned<T>(int userId);
        Task<int> Add(CreateGamemodeDto createGamemodeDto, int userId);
        Task Delete(int id, int userId, bool skipUserVerification = false);
        Task<int> Update(int id, EditGamemodeDto editGamemodeDto, int userId, bool skipUserVerification = false);
    }
}
