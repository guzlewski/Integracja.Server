 using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Home
{
    public interface IHomeActions
    {
        Task<IActionResult> GotoQuestionCreate();
        Task<IActionResult> GotoQuestionRead(int? id);
        Task<IActionResult> GotoQuestionUpdate(int? id);
        Task<IActionResult> GotoQuestionDelete(int? id);
    }
}
