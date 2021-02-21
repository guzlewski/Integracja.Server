using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models
{
    /* służy reprezentacji formy w widoku i w kontrolerze */
    public class QuestionFormModel
    {
        public string QuestionContent { get; set; }
        public List<(string Content, bool isCorrect)> Answers { get; set; }
        public int PositivePoints { get; set; }
        public int NegativePoints { get; set; }
        public QuestionScoring Scoring { get; set; }
        public int CategoryId { get; set; }

        public QuestionFormModel()
        {
        }
        public QuestionFormModel(int answersCount)
        {
            Answers = new List<(string, bool)>(answersCount);
        }
    }

    public class QuestionViewModel : PageModel
    {
        public string Controller { get; private set; }
        public string Title { get; private set; }
        public bool EditMode { get; private set; }

        [BindProperty]
        public QuestionFormModel QuestionForm { get; set; }

        public const int DefaultAnswerCount = 4;

        public QuestionViewModel( string title, bool editMode, string controllerName, int answerCount = DefaultAnswerCount ) : base()
        {
            Controller = controllerName;
            Title = title;
            EditMode = editMode;

            /* forma dla widoku zawiera listę więc lista musi być zainicjalizowana elementami żeby można było je wyświetlić i wypełnić */
            QuestionForm = new QuestionFormModel(answerCount);
        }
    }
}
