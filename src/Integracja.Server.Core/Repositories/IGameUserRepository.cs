using System;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameUserRepository
    {
        IQueryable<GameUser> Get(int gameId, int userId);
        IQueryable<GameUser> GetAll(int gameId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
    }
}
