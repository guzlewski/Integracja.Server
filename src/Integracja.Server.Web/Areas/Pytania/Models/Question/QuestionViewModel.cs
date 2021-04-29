using System.Collections.Generic;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public class QuestionViewModel
    {
        public List<AlertModel> Alerts { get; set; } = new();

        public QuestionFormViewModel Form { get; set; } = new QuestionFormViewModel();

        public string GoBackActionName { get; set; } = "";

        public QuestionViewModel()
        {
        }

        public QuestionViewModel(QuestionModel question, List<AlertModel> alerts)
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
