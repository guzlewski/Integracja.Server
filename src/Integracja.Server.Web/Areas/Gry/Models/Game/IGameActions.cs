using System.Threading.Tasks;
using Integracja.Server.Web.Areas.Gry.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Models.Game
{
    public interface IGameActions : IGameQuestionsFormActions, IGameSettingsFormActions
    {
        Task<IActionResult> SettingsCreateView(int gamemodeId);
        Task<IActionResult> QuestionPoolCreateView();
        Task<IActionResult> GameCreate();
        Task<IActionResult> GameRead(int gameId);
        Task<IActionResult> GameDelete(int gameId);
    }
}
