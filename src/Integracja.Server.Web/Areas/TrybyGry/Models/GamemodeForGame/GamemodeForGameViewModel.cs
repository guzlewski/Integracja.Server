﻿using System.Collections.Generic;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Gamemode;

namespace Integracja.Server.Web.Areas.TrybyGry.Models.GamemodeForGame
{
    public class GamemodeForGameViewModel
    {
        public List<GamemodeModel> Gamemodes { get; set; } = new List<GamemodeModel>();

        public List<AlertModel> Alerts { get; set; }

        public int? SelectedGamemode { get; set; }

        public GamemodeForGameViewModel()
        {

        }
    }
}
