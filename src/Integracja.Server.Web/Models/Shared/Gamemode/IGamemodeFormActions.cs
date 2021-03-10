using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Gamemode
{
    public interface IGamemodeFormActions
    {
        Task<IActionResult> GamemodeCreate(GamemodeModel gamemode);

        Task<IActionResult> GamemodeUpdate(GamemodeModel gamemode);

    }
}
