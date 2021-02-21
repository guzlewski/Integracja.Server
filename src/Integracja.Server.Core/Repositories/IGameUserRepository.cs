using System;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameUserRepository
    {
        Task Accept(int gameId, int userId);
        Task<int> Join(Guid gameGuid, int userId);
        Task Leave(int gameId, int userId);
        IQueryable<GameUser> Get(int gameId, int userId);
        IQueryable<GameUser> GetAll(int userId);
    }
}
