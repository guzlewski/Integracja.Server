using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public interface IQuestionTableActions
    {
        Task<IActionResult> GotoQuestionRead(int questionId);
        Task<IActionResult> GotoQuestionUpdate(int questionId);
        Task<IActionResult> GotoQuestionDelete(int questionId, int categoryId);
    }
}
