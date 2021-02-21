using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Services
{
    public interface IGameQuestionService
    {
        Task<GameQuestionGet> GetQuestion(int gameId, int userId);
        Task<GameUserQuestionGet> SaveAnswers(int gameId, int userId, ICollection<int> answers);
    }
}
