using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Gry.Models.Home;
using Integracja.Server.Web.Areas.TrybyGry.Controllers;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> GotoGameCreate()
        {
            return RedirectToAction("Index", GamemodeForGameController.Name, new { area = "TrybyGry" } );
        }

        public Task<IActionResult> GotoGameDelete()
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> GotoGameRead()
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> GotoGameUpdate()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
