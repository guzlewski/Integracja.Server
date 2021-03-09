using Integracja.Server.Web.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Models.Shared.Question
{

    public class QuestionFormViewModel : PageModel
    {
        [BindProperty]
        public QuestionModel Question { get; set; }
        public ViewMode ViewMode { get; set; }

        public QuestionFormViewModel() : base()
        {
            ViewMode = ViewMode.Reading;
            Question = new QuestionModel();
        }
        public QuestionFormViewModel(ViewMode mode) : base()
        {
            if (mode == ViewMode.Deleting)
                throw new System.NotImplementedException();
            ViewMode = mode;
            Question = new QuestionModel();
        }
        public QuestionFormViewModel(QuestionModel question)
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
