using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
{
    public interface IHomeNav
    {
        Task<IActionResult> AllGames();
        Task<IActionResult> CurrentGames();
        Task<IActionResult> EndedGames();
        Task<IActionResult> GotoGameCreate();
    }
}
