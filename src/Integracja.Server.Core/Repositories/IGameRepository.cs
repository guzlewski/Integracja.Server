using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameRepository
    {
        IQueryable<Game> Get(int id, int userId);
        IQueryable<Game> GetAll(int userId);
        Task<int> Add(Game game, int questionsCount, bool randomizeQuestionOrder = false);
        Task Delete(Game game);
        Task<int> Update(Game game);
    }
}
