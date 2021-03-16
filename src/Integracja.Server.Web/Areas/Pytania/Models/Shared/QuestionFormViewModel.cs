using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{

    public class QuestionFormViewModel
    {
        [BindProperty]
        public QuestionModel Question { get; set; } = new QuestionModel();
        public ViewMode ViewMode { get; set; } = ViewMode.Reading;

        public QuestionFormViewModel()
        {
        }

        public QuestionFormViewModel(ViewMode mode)
        {
            ViewMode = mode;
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
