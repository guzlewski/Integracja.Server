﻿using System.Threading.Tasks;
using Integracja.Server.Web.Models.Shared.Game;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Models.Shared
{
    public interface IGameSettingsFormActions
    {
        Task<IActionResult> GameSettingsCreate(GameSettingsModel settings);
    }
}
