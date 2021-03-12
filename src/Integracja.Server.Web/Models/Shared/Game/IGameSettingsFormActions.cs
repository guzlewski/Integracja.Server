using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public interface IGameSettingsFormActions
    {
        Task<IActionResult> GameSettingsCreate(GameSettingsModel settings);
    }
}
