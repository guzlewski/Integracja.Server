using System.Collections.Generic;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Repositories
{
    public interface IGameQuestionRepository
    {
        public Task<GameQuestion> GetQuestion(int gameId, int userId);
        public Task<GameUserQuestion> SaveAnswers(int gameId, int userId, ICollection<int> answers);
    }
}
