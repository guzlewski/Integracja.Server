using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IGameLogicService
    {
        Task<GameQuestionDto> GetQuestion(int gameId, int userId);
        Task<GameUserQuestionDto> SaveAnswers(int gameId, int userId, int questionId, IEnumerable<int> answers);
    }
}
