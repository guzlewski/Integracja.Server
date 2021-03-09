using Integracja.Server.Web.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Models.Shared.Question
{

    public class QuestionFormViewModel : PageModel
    {
        [BindProperty]
        public QuestionModel Question { get; set; } = new QuestionModel();
        public ViewMode ViewMode { get; set; } = ViewMode.Reading;

        public QuestionFormViewModel() : base()
        {
        }

        public QuestionFormViewModel(ViewMode mode) : base()
        {
            ViewMode = mode;
        }

        public QuestionFormViewModel(QuestionModel question) : base()
        {
            this.Question = question;
            if (question.Id.HasValue)
                this.ViewMode = ViewMode.Updating;
            else this.ViewMode = ViewMode.Creating;
        }

        public static class Ids
        {
            public const string Form = "question_form";
        }
    }
}
