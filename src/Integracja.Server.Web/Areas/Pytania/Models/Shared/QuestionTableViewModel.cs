using Integracja.Server.Web.Models.Shared.Question;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public class QuestionTableViewModel
    {
        public List<QuestionModel> Questions;
        public bool ReadActionsOnly { get; set; } = false;
    }
}
