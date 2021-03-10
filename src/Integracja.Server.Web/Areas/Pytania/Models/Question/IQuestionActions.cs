using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public interface IQuestionActions : IQuestionFormActions
    {
        Task<IActionResult> QuestionCreateViewStep1();
        Task<IActionResult> QuestionCreateViewStep2(int categoryId);
        Task<IActionResult> QuestionReadView(int? questionId );
        Task<IActionResult> QuestionUpdateView(int? questionId);
        Task<IActionResult> QuestionDelete(int? questionId);
    }
}
