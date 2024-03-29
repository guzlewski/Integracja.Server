﻿using System.Threading.Tasks;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.TrybyGry.Models.Shared
{
    public interface IGamemodeFormActions
    {
        Task<IActionResult> GamemodeCreate(GamemodeModel gamemode);

        Task<IActionResult> GamemodeUpdate(GamemodeModel gamemode);

    }
}
