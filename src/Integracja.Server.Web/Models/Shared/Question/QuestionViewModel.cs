using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Question
{

    public class QuestionViewModel : PageModel
    {
        public string Controller { get; private set; }
        public bool EditMode { get; private set; }

        [BindProperty]
        public QuestionModel Question { get; set; }

        // EditMode miałby służyć wyświetlaniu widoku w wersji tylko do przeglądu dla użytkownika np.: dla strony przeglądania pytań
        // nie wiem do końca jeszcze czy takie coś będzie, w jakiej postaci itd. więc na razie to martwy kod
        public QuestionViewModel( bool editMode = true, string controllerName = "") : base()
        {
            Controller = controllerName;
            EditMode = editMode;

            Question = new QuestionModel();
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
