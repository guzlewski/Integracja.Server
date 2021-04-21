using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public interface IQuestionCardActions
    {
        Task<IActionResult> QuestionReadView(int questionId);
        Task<IActionResult> GotoQuestionUpdate(int questionId);
        Task<IActionResult> GotoQuestionDelete(int questionId);
        Task<IActionResult> GotoHome(int? categoryId);
    }
}
