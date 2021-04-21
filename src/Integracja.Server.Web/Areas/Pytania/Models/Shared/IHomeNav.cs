using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public interface IHomeNav
    {
        Task<IActionResult> GotoOwnedQuestions();
        Task<IActionResult> GotoAllQuestions();
        Task<IActionResult> GotoOwnedCategories();
        Task<IActionResult> GotoQuestionCreate();
    }
}
