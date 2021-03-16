using Integracja.Server.Web.Areas.Gry.Models.Shared;
using Integracja.Server.Web.Models.Shared.Game;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Game
{
    public interface IGameActions : IGameQuestionFormActions, IGameSettingsFormActions
    {
        Task<IActionResult> SettingsCreateView(int? gamemodeId);

        Task<IActionResult> QuestionPoolCreateView();

        Task<IActionResult> GameCreate();
    }
}
