using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Integracja.Server.Infrastructure.Settings;
using Integracja.Server.Web.Areas.Konto.Models;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Web.Areas.Konto.Controllers
{
    [Area("Konto")]
    public class HomeController : ApplicationController, IHomeActions
    {
        private HomeViewModel Model { get; set; }
        
        private readonly IPictureService _pictureService;
        private readonly PictureSettings _pictureSettings;

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper, IPictureService pictureService, IOptions<PictureSettings> options) : base(userManager, dbContext, mapper)
        {
            Model = new HomeViewModel();
            _pictureService = pictureService;
            _pictureSettings = options.Value;
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
            if (file != null)
            {
                try
                {
                    await _pictureService.Save(file, UserId);
                }
                catch (PayloadTooLargeException)
                {
                    ModelState.AddModelError(nameof(file), $"Zbyt duży opbrazek, maksymalna wielkość {_pictureSettings.MaxSize / 1024} KB");
                }
                catch (UnsupportedMediaTypeException)
                {
                    ModelState.AddModelError(nameof(file), "Nieobsługiwany typ obrazka");
                }
                catch (UnprocessableEntityException)
                {
                    ModelState.AddModelError(nameof(file), "Nieprawidłowy plik");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(file), "Nie wysłano żadnego pliku");
            }

            return View("Index", Model);
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
