using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Question
{
    interface IQuestionPartialActions
    {
        public const string NameOfQuestionCreate = nameof(IQuestionPartialActions.QuestionCreate);
        public const string NameOfQuestionUpdate = nameof(IQuestionPartialActions.QuestionUpdate);
        public const string NameOfAddAnswerField = nameof(IQuestionPartialActions.AddAnswerField);
        public const string NameOfRemoveAnswerField = nameof(IQuestionPartialActions.RemoveAnswerField);

        Task<IActionResult> AddAnswerField(
        int? categoryId,
        QuestionModel question);

        Task<IActionResult> RemoveAnswerField(
        int? categoryId,
        QuestionModel question);

        Task<IActionResult> QuestionCreate(
        int? categoryId,
        QuestionModel question);

        Task<IActionResult> QuestionUpdate(
        int? categoryId,
        QuestionModel question);
    }
}
