using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public interface IHomeNav
    {
        Task<IActionResult> MyQuestions();
        Task<IActionResult> AllQuestions();
        Task<IActionResult> MyCategories();
        Task<IActionResult> GotoQuestionCreate();
    }
}
