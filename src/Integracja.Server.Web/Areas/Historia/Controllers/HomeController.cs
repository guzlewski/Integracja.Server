using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Historia.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.History;
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

            HistoryUserModel users = await GameService.Get<HistoryUserModel>(gameId, UserId);

            List<HistoryGameUser> historyuser = new List<HistoryGameUser>();
            HistoryGameUser guser = new HistoryGameUser();
            foreach(var element in users.GameUsers)
            {
                var user = await UserManager.FindByIdAsync(element.UserId.ToString());
                guser.gameuser = element;
                guser.Username = user.UserName;
                historyuser.Add(guser);
            }

            Model.GameUsers = historyuser;      

            return View(Model);
        }
    }
}
