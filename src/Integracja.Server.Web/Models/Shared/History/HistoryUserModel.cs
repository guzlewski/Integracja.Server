using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Web.Models.Shared.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.History
{
    public class HistoryUserModel
    {
        public ICollection<GameUser> GameUsers { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public List<UserAnswerModel> UserAnswerPool { get; set; } = new List<UserAnswerModel>();

    }
}
