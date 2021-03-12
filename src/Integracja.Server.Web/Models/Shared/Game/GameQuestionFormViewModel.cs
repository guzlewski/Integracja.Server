using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameQuestionFormViewModel
    {
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
        public List<QuestionModel> GameQuestions { get; set; } = new List<QuestionModel>();
    }
}
