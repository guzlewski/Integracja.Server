using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public class QuestionViewModel : QuestionPartialViewModel
    {
        public QuestionViewModel() : base()
        {
        }

        public QuestionViewModel(ViewMode mode) : base(mode)
        {
        }

        public QuestionViewModel(QuestionModel question)
        {
            this.Question = question;
            if (question.Id.HasValue)
                this.ViewMode = ViewMode.Updating;
            else this.ViewMode = ViewMode.Creating;
        }
    }
}
