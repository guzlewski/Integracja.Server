using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public class QuestionViewModel : QuestionFormViewModel
    {
        public QuestionAlert Alert { get; set; }

        public QuestionViewModel() : base()
        {
        }

        public QuestionViewModel(ViewMode mode) : base(mode)
        {
        }

        public QuestionViewModel(QuestionModel question) : base(question)
        {
        }

        public QuestionViewModel(QuestionModel question, QuestionAlert alert ) : base(question)
        {
            Alert = alert;
        }
    }
}
