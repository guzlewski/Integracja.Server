using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Kategorie;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllers.PanelAdmina.Kategorie
{
    public class KategorieController : ApplicationController
    {
        private KategorieViewModel Model { get; set; }

        public KategorieController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new KategorieViewModel();
        }

        public IActionResult Index()
        {
            Model.Categories = (System.Collections.Generic.List<Infrastructure.DTO.CategoryGetAll>)CategoryService.GetAll(UserId).Result;
            return View("~/Views/PanelAdmina/Kategorie/CategoriesTable.cshtml", Model);
        }
    }
}
