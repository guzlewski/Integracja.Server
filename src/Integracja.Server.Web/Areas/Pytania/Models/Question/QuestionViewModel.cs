using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public class QuestionViewModel
    {
        public QuestionAlert Alert { get; set; }

        public QuestionFormViewModel Form { get; set; } = new QuestionFormViewModel();

        public QuestionViewModel()
        {
        }

        public QuestionViewModel( QuestionModel question, QuestionAlert alert )
        {
            Form = new QuestionFormViewModel(question);
            //Form.Question = question;
            Alert = alert;
        }

        public QuestionViewModel(ViewMode mode)
        {
            Form = new QuestionFormViewModel(mode);
        }
    }
}
