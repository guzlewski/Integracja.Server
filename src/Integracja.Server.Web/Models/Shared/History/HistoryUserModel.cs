using System.Collections.Generic;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Web.Models.Shared.Answer;

namespace Integracja.Server.Web.Models.Shared.History
{
    public class HistoryUserModel
    {
        public ICollection<GameUser> GameUsers { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public List<UserAnswerModel> UserAnswerPool { get; set; } = new List<UserAnswerModel>();

    }
}
