using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
