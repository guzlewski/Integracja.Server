using Integracja.Server.Web.Models.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models
{

    public class QuestionViewModel : PageModel
    {
        public string Controller { get; private set; }
        public string Title { get; private set; }
        public bool EditMode { get; private set; }

        [BindProperty]
        public QuestionModel Question { get; set; }

        public QuestionViewModel( string title, bool editMode, string controllerName, int answerCount = DefaultAnswerCount ) : base()
        {
            Controller = controllerName;
            Title = title;
            EditMode = editMode;

            /* model dla widoku zawiera listę więc lista musi być zainicjalizowana elementami żeby można było je wyświetlić i wypełnić */
            Question = new QuestionModel(answerCount);
        }

        public const int DefaultAnswerCount = 4;
        public const string FormId = "QuestionFormId";

        public static class ActionNames
        {
            public const string ProcessForm = nameof(IActions.ProcessQuestionForm);
            public const string AddAnswerField = nameof(IActions.AddAnswerField);
            public const string RemoveAnswerField = nameof(IActions.RemoveAnswerField);
        }
        public interface IActions
        {
            Task<IActionResult> AddAnswerField(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);

            Task<IActionResult> RemoveAnswerField(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);

            Task<IActionResult> ProcessQuestionForm(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);
        }
    }
}
