using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public interface IGameFormActions
    {
        Task<IActionResult> GameCreate(GameModel game);

        Task<IActionResult> GameUpdate(GameModel game);

    }
}
