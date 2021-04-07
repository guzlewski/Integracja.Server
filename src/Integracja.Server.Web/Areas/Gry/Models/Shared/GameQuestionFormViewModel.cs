using System.Collections.Generic;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
{
    public class GameQuestionFormViewModel
    {
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
        public List<QuestionModel> GameQuestions { get; set; } = new List<QuestionModel>();
    }
}
