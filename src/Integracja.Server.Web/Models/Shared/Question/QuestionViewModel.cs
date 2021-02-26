using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Question
{

    public class QuestionViewModel : PageModel
    {
        public string Controller { get; private set; }
        public string Title { get; private set; }
        public bool EditMode { get; private set; }

        [BindProperty]
        public QuestionModel Question { get; set; }

        public const int DefaultAnswerCount = 4;

        public QuestionViewModel(string title, bool editMode, string controllerName, int answerCount = DefaultAnswerCount) : base()
        {
            Controller = controllerName;
            Title = title;
            EditMode = editMode;

            Question = new QuestionModel(answerCount);
        }
        
        public const string FormId = "QuestionFormId";

        public static class ActionNames
        {
            public const string ProcessForm = nameof(IActions.ProcessQuestionForm);
            public const string AddAnswerField = nameof(IActions.AddAnswerField);
            public const string RemoveAnswerField = nameof(IActions.RemoveAnswerField);
        }

        public interface IActions
        {
            // categoryId jest potrzebne żeby kontroler mógł zapamiętać kategorię
            // bo gdy tworzone jest pytanie to id nie jest przypisane z modelu
            Task<IActionResult> AddAnswerField(
            int? categoryId,
            [Bind(Prefix = nameof(Question))] QuestionModel question);

            Task<IActionResult> RemoveAnswerField(
            int? categoryId,
            [Bind(Prefix = nameof(Question))] QuestionModel question);

            Task<IActionResult> ProcessQuestionForm(
            int? categoryId,
            [Bind(Prefix = nameof(Question))] QuestionModel question);
        }
    }
}
