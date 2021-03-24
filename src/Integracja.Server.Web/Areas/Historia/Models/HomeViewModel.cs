using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.Gamemode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Historia.Models
{
    public class HomeViewModel
    {
        public GameModel Game { get; set; }

        public GamemodeModel Gamemode { get; set; }
    }
}
