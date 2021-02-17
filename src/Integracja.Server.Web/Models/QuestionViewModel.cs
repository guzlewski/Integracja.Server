using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models
{
    public class QuestionViewModel : PageModel
    {
        public QuestionAdd Question { get; set; }
        public List<AnswerDto> Answers { get; set; }
        public string Title { get; set; }
        public bool EditMode { get; set; }

        // dla mozliwosci rozszerzenia pól ?
        public const int DefaultAnswerCount = 4;
        public int AnswerCount { get; set; }

        public QuestionViewModel( string title, bool editMode, int answerCount = DefaultAnswerCount ) : base()
        {
            Title = title;
            EditMode = editMode;
            AnswerCount = answerCount;

            Question = new QuestionAdd();
            Answers = new List<AnswerDto>();
            for (int i = 0; i < AnswerCount; ++i)
                Answers.Add(new AnswerDto());
        }
    }
}
