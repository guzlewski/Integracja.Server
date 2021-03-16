using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public interface IQuestionFormActions
    {
        public const string NameOfQuestionCreate = nameof(IQuestionFormActions.QuestionCreate);
        public const string NameOfQuestionUpdate = nameof(IQuestionFormActions.QuestionUpdate);
        public const string NameOfAddAnswerField = nameof(IQuestionFormActions.AddAnswerField);
        public const string NameOfRemoveAnswerField = nameof(IQuestionFormActions.RemoveAnswerField);

        Task<IActionResult> AddAnswerField(
        QuestionModel question);

        Task<IActionResult> RemoveAnswerField(
        QuestionModel question);

        Task<IActionResult> QuestionCreate(
        QuestionModel question);

        Task<IActionResult> QuestionUpdate(
        QuestionModel question);
    }
}
