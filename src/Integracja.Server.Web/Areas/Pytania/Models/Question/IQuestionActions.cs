using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public interface IQuestionActions : IQuestionFormActions, IQuestionCardActions
    {
        Task<IActionResult> QuestionCreateViewStep1(int? categoryId);
        Task<IActionResult> QuestionCreateViewStep2(int categoryId);
        Task<IActionResult> QuestionUpdateView(int? questionId);
        Task<IActionResult> QuestionDelete(int? questionId);
        Task<IActionResult> QuestionCreateCategoryUpdate(int categoryId);
        Task<IActionResult> GotoHome();

    }
}
