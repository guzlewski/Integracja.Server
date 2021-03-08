using Integracja.Server.Web.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Models.Shared.Question
{

    public class QuestionPartialViewModel : PageModel
    {
        [BindProperty]
        public QuestionModel Question { get; set; }
        public ViewMode ViewMode { get; set; }

        public QuestionPartialViewModel() : base()
        {
            ViewMode = ViewMode.Reading;
            Question = new QuestionModel();
        }
        public QuestionPartialViewModel(ViewMode mode) : base()
        {
            if (mode == ViewMode.Deleting)
                throw new System.NotImplementedException();
            ViewMode = mode;
            Question = new QuestionModel();
        }
        public QuestionPartialViewModel(QuestionModel question)
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
