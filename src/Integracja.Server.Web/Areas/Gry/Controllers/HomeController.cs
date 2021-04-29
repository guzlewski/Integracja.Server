using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Gry.Models.Game;
using Integracja.Server.Web.Areas.Gry.Models.Home;
using Integracja.Server.Web.Areas.Gry.Models.Shared;
using Integracja.Server.Web.Areas.TrybyGry.Controllers;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Game;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Controllers
{
    [Area("Gry")]
    public class HomeController : ApplicationController, IHomeActions, IHomeNav
    {
        public static new string Name = "Home";

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IHomeNav.CurrentGames)));
        }

        public Task<IActionResult> GotoGameCreate()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", GamemodeForGameController.Name, new { area = "TrybyGry" }));
        }

        public Task<IActionResult> GotoGameDelete(int gameId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IGameActions.GameDelete), GameController.Name, new { gameId = gameId }));
        }

        public Task<IActionResult> GotoGameRead(int gameId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IGameActions.GameRead), GameController.Name, new { gameId = gameId }));
        }

        public Task<IActionResult> GotoGameUpdate(int gameId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> GotoGameHistory(int gameId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", "Home", new { area = "Historia", gameId }));
        }

        public async Task<IActionResult> AllGames()
        {
            HomeViewModel model = new HomeViewModel();
            var gamesDto = await GameService.GetAll<GameDto>(UserId);
            model.Games = Mapper.Map<List<GameModel>>(gamesDto);
            model.Alerts = GetAlerts();
            return View(model);
        }

        public async Task<IActionResult> CurrentGames()
        {
            HomeViewModel model = new HomeViewModel();
            var gamesDto = await GameService.GetCurrent<GameDto>(UserId);
            model.Games = Mapper.Map<List<GameModel>>(gamesDto);
            model.Alerts = GetAlerts();
            return View(model);
        }

        public async Task<IActionResult> EndedGames()
        {
            HomeViewModel model = new HomeViewModel();
            var gamesDto = await GameService.GetEnded<GameDto>(UserId);
            model.Games = Mapper.Map<List<GameModel>>(gamesDto);
            model.Alerts = GetAlerts();
            return View(model);
        }
    }
}
