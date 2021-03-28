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
using Microsoft.AspNetCore.Routing;
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
            foreach (var element in users.GameUsers)
            {
                var user = await UserManager.FindByIdAsync(element.UserId.ToString());

                HistoryGameUser guser = new HistoryGameUser
                {
                    gameuser = element,
                    Username = user.UserName,
                    place = 1
                };

                historyuser.Add(guser);
            }

            Model.GameUsers = Sort(historyuser);

            return View(Model);
        }

        public List<HistoryGameUser> Sort(List<HistoryGameUser> historyuser)
        {
            List<HistoryGameUser> SortedList = historyuser.OrderByDescending(o => o.gameuser.GameScore).ToList();

            for (int i = 1; i < SortedList.Count; i++)
                if (SortedList[i].gameuser.GameScore == SortedList[i - 1].gameuser.GameScore)
                    SortedList[i].place = SortedList[i - 1].place;
                else
                    SortedList[i].place = SortedList[i - 1].place + 1;

            return SortedList;
        }

        public async Task<IActionResult> HistoryUserReadView(int gameId, int userId)
        {
            return RedirectToAction("Index", "HistoryUser", new { gameId, userId });
        }

        public async Task<IActionResult> HistoryQuestionReadView(int questionId)
        {
            return RedirectToAction("Index", "HistoryQuestion", new { questionId });
        }

    }
}
