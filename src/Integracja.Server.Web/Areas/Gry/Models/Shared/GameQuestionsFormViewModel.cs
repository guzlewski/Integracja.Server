using System.Collections.Generic;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
{
    public class GameQuestionsFormViewModel
    {
        public List<QuestionModel> Questions { get; set; } = new ();
        public List<int> GameQuestions { get; set; } = new();
    }
}
