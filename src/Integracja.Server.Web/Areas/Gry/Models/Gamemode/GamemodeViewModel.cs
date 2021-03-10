using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Gry.Models.Gamemode
{
    public class GamemodeViewModel : PageModel
    {
        public List<GamemodeModel> Gamemodes { get; set; } = new List<GamemodeModel>();

        public int? SelectedGamemode { get; set; }

        public GamemodeViewModel()
        {

        }
    }
}
