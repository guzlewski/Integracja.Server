using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Home;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    [Area("PanelAdmina")]
    public class HomeController : ApplicationController
    {
        private HomeViewModel Model { get; set; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new HomeViewModel();
        }

        public IActionResult Index()
        {
            return View("Index", Model);
        }

        public FileContentResult Picture()
        {
            var user = UserManager.GetUserAsync(User);

            return new FileContentResult(user.Result.Picture, "image/jpeg");
        }
    }
}
