using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameLogicService
    {
        Task<T> GetQuestion<T>(int gameId, int userId);
        Task<T> SaveAnswers<T>(int gameId, int userId, int questionId, IEnumerable<int> answers);
    }
}
