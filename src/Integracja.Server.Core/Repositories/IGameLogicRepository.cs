using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameLogicRepository
    {
        Task<IQueryable<GameUserQuestion>> GetQuestion(int gameId, int userId);
        Task<IQueryable<GameUserQuestion>> SaveAnswers(int gameId, int userId, int questionId, IEnumerable<int> asnwers);
    }
}
