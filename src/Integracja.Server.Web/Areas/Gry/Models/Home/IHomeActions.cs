using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Home
{
    public interface IHomeActions
    {
        Task<IActionResult> GotoGameCreate();
        Task<IActionResult> GotoGameRead();
        Task<IActionResult> GotoGameUpdate();
        Task<IActionResult> GotoGameDelete();
    }
}
