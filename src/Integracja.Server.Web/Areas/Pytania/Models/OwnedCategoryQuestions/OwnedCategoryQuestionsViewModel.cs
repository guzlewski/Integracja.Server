using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Question;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Pytania.Models.OwnedCategoryQuestions
{
    public class OwnedCategoryQuestionsViewModel
    {
        public List<QuestionModel> Questions { get; set; }
        public List<AlertModel> Alerts { get; set; }
    }
}
