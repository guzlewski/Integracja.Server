using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Question
{
    public interface IQuestionCardActions
    {
        Task<IActionResult> QuestionReadView(int questionId);
        Task<IActionResult> GotoQuestionUpdate(int questionId);
        Task<IActionResult> GotoQuestionDelete(int questionId);
    }
}
