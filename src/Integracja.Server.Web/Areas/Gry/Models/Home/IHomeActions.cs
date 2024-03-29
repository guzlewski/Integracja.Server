﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Models.Home
{
    public interface IHomeActions
    {
        Task<IActionResult> GotoGameCreate();
        Task<IActionResult> GotoGameHistory(int gameId);
        Task<IActionResult> GotoGameRead(int gameId);
        Task<IActionResult> GotoGameUpdate(int gameId);
        Task<IActionResult> GotoGameDelete(int gameId);
    }
}
