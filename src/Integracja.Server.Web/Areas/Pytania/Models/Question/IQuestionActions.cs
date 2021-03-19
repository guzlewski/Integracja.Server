using Integracja.Server.Web.Areas.Pytania.Models.QuestionCard;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Models.Shared.Question;
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
    }
}
