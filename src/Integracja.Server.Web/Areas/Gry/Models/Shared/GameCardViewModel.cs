using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.Gamemode;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
{
    public class GameCardViewModel
    {
        public GameModel Game { get; set; }

        public GamemodeModel Gamemode { get; set; }
    }
}
