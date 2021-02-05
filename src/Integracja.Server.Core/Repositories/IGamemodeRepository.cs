using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IGamemodeRepository
    {
        IQueryable<Gamemode> Get(int id, int userId);
        IQueryable<Gamemode> GetAll(int userId);
        Task<int> Add(Gamemode gamemode);
        Task Delete(Gamemode gamemode);
        Task<int> Update(Gamemode gamemode);
    }
}
