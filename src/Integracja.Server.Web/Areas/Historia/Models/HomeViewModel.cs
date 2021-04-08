using System.Collections.Generic;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.Gamemode;

namespace Integracja.Server.Web.Areas.Historia.Models
{
    public class HomeViewModel
    {
        public GameModel Game { get; set; }
        public GamemodeModel Gamemode { get; set; }
        public List<HistoryGameUser> GameUsers { get; set; }
    }

    public class HistoryGameUser
    {
        public GameUser gameuser;
        public string Username;
        public int place;
    }
}
