using System.Collections.Generic;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Game;

namespace Integracja.Server.Web.Areas.Gry.Models.Home
{
    public class HomeViewModel
    {
        public List<GameModel> Games { get; set; } = new List<GameModel>();

        public List<AlertModel> Alerts { get; set; }
    }
}
