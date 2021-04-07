using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public class QuestionViewModel
    {
        public List<AlertModel> Alerts { get; set; }

        public QuestionFormViewModel Form { get; set; } = new QuestionFormViewModel();

        public QuestionViewModel()
        {
        }

        public QuestionViewModel( QuestionModel question, List<AlertModel> alerts )
        {
            Form = new QuestionFormViewModel(question);
            Alerts = alerts;
        }

        public QuestionViewModel(ViewMode mode)
        {
            Form = new QuestionFormViewModel(mode);
        }
    }
}
