﻿using System.Threading.Tasks;
using Integracja.Server.Web.Areas.TrybyGry.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.TrybyGry.Models.GamemodeForGame
{
    public interface IGamemodeForGameActions : IGamemodeFormActions
    {
        Task<IActionResult> GamemodeRead(int? id);
        Task<IActionResult> GamemodeDelete(int? id);
        Task<IActionResult> GamemodeUpdateView(int? id);
        Task<IActionResult> GamemodeCreateView();
        Task<IActionResult> GotoGameCreate(int? gamemodeId);
    }
}
