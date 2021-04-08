using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Home;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    [Area("PanelAdmina")]
    public class HomeController : ApplicationController, IHomeActions
    {
        private HomeViewModel Model { get; set; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
            Model = new HomeViewModel();
        }

        public IActionResult Index()
        {
            return View("Index", Model);
        }

        public Task<IActionResult> GotoPytaniaAdminHome()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", Pytania.Controllers.AdminHomeController.Name, new { area = "Pytania" }));
        }

        public Task<IActionResult> GotoKategorieAdminHome()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", Kategorie.Controllers.AdminHomeController.Name, new { area = "Kategorie" }));
        }
    }
}
