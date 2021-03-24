using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Gry.Models.Game;
using Integracja.Server.Web.Areas.Gry.Models.Home;
using Integracja.Server.Web.Areas.TrybyGry.Controllers;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Mapper;
using Integracja.Server.Web.Models.Shared.Game;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Controllers
{
    [Area("Gry")]
    public class HomeController : ApplicationController, IHomeActions
    {
        public static new string Name = "Home";

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }
        
        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            var gamesDto = await GameService.GetAll<GameDto>(UserId);
            model.Games = Mapper.Map<List<GameModel>>(gamesDto);
            model.Alerts = GetAlerts();
            return View(model);
        }

        public async Task<IActionResult> GotoGameCreate()
        {
            return RedirectToAction("Index", GamemodeForGameController.Name, new { area = "TrybyGry" } );
        }

        public async Task<IActionResult> GotoGameDelete(int gameId)
        {
            return RedirectToAction(nameof(IGameActions.GameDelete), GameController.Name, new { gameId = gameId });
        }

        public async Task<IActionResult> GotoGameRead(int gameId)
        {
            return RedirectToAction(nameof(IGameActions.GameRead), GameController.Name, new { gameId = gameId });
        }

        public Task<IActionResult> GotoGameUpdate(int gameId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> GotoGameHistory(int gameId)
        {
            return RedirectToAction("Index", "Home", new { area = "Historia", gameId });
        }
    }
}
