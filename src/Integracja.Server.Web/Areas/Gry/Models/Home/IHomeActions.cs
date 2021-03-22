using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Home
{
    public interface IHomeActions
    {
        Task<IActionResult> GotoGameCreate();
        Task<IActionResult> GotoGameRead( int gameId );
        Task<IActionResult> GotoGameUpdate( int gameId );
        Task<IActionResult> GotoGameDelete( int gameId);
    }
}
