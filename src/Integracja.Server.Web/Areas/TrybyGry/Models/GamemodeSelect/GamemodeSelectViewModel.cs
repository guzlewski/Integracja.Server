using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.TrybyGry.Models.GamemodeSelect
{
    public class GamemodeSelectViewModel : PageModel
    {
        public List<GamemodeModel> Gamemodes { get; set; } = new List<GamemodeModel>();

        public int? SelectedGamemode { get; set; }

        public GamemodeSelectViewModel()
        {

        }
    }
}
