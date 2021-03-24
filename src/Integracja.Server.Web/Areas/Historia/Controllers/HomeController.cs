using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Historia.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Game;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Historia.Controllers
{
    [Area("Historia")]
    public class HomeController : ApplicationController
    {

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int gameId)
        {
            HomeViewModel Model = new HomeViewModel();
            GameModel game = await GameService.Get<GameModel>(gameId, UserId);

            Model.Game = game;
            Model.Gamemode = game.Settings.Gamemode;

            return View(Model);
        }
    }
}
