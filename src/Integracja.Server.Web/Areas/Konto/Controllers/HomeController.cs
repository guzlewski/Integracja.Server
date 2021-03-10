using AutoMapper;
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
    public class HomeController : ApplicationController, IHomeActions
    {
        private HomeViewModel Model { get; set; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
            Model = new HomeViewModel();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    if (memoryStream.Length < 209715211)
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

            return RedirectToAction("Index");
        }
    }
}
