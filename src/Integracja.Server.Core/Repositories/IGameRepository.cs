using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameRepository
    {
        IQueryable<Game> Get(int id);
        IQueryable<Game> GetAll();
        Task<int> Add(Game game, bool randomizeQuestionOrder = false);
        Task Delete(Game game);
        Task<int> Update(Game game);
    }
}
