using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Gamemode
{
    public interface IGamemodeActions : IGamemodeFormActions
    {
        Task<IActionResult> GamemodeRead(int? id);
        Task<IActionResult> GamemodeDelete(int? id);
        Task<IActionResult> GamemodeUpdateView(int? id);
        Task<IActionResult> GamemodeCreateView();
    }
}
