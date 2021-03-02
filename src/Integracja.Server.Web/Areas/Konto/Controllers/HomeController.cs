using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Konto.Models;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Konto.Controllers
{
    [Area("Konto")]
    public class HomeController : ApplicationController, HomeViewModel.IActions
    {
        private HomeViewModel Model { get; set; }
        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new HomeViewModel();
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public FileContentResult Picture()
        {
            var user = UserManager.GetUserAsync(User);

            return new FileContentResult(user.Result.Picture, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    if (memoryStream.Length < 20971521111111111)
                    {
                        var user = await UserManager.FindByNameAsync(User.Identity.Name);
                        user.Picture = memoryStream.ToArray();
                        await UserManager.UpdateAsync(user);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Zdjęcie może mieć co najwyżej 2MB");
                        return View("Index");
                    }
                }
            }

            return RedirectToAction("Index", "Konto");
        }
    }
}
