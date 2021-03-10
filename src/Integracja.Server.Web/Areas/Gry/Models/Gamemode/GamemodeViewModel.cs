using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Gamemode
{
    public class GamemodeViewModel : PageModel
    {
        public List<GamemodeModel> Gamemodes { get; set; } = new List<GamemodeModel>();

        public GamemodeViewModel()
        {

        }
    }
}
