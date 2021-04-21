using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameRepository
    {
        IQueryable<Game> Get(int id);
        IQueryable<Game> GetAll();
        Task<int> Add(Game game, bool randomizeQuestionOrder = false, bool skipUserVerification = false);
        Task Delete(Game game, bool skipUserVerification = false);
        Task<int> Update(Game game, bool skipUserVerification = false);
    }
}
