using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Konto.Models;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
            
            Model.Details.Username = User.Identity.Name;

            var user = UserManager.FindByNameAsync(User.Identity.Name);
            Model.Details.Email = user.Result.Email;

            if (user.Result.PhoneNumber != null)
                Model.Details.PhoneNumber = user.Result.PhoneNumber;

            Model.Details.EmailConfirmed = user.Result.EmailConfirmed;
            Model.Details.PhoneNumberConfirmed = user.Result.PhoneNumberConfirmed;
            
            return View(Model);
        }


        [HttpPost]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    if (memoryStream.Length < 2097152)
                    {
                        var user = await UserManager.FindByNameAsync(User.Identity.Name);
                        // TODO:
                        //user.Picture = memoryStream.ToArray();
                        await UserManager.UpdateAsync(user);
                        throw new NotImplementedException();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Zdjęcie może mieć co najwyżej 2MB");
                        return View("Index", Model);
                    }
                }
            }

            return RedirectToAction("Index", Model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail(string Email)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            user.Email = Email;
           
            await UserManager.UpdateAsync(user);

            return RedirectToAction("Index", Model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoneNumber(string PhoneNumber)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            user.PhoneNumber = PhoneNumber;

            await UserManager.UpdateAsync(user);

            return RedirectToAction("Index", Model);
        }
    }
}
