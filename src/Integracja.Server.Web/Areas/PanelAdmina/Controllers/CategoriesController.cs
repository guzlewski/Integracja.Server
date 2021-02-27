using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Categories;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    public class CategoriesController : ApplicationController
    {
        private CategoriesViewModel Model { get; set; }

        public CategoriesController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new CategoriesViewModel();
        }

        public IActionResult Index()
        {
            Model.Categories = (System.Collections.Generic.List<Infrastructure.DTO.CategoryGetAll>)CategoryService.GetAll(UserId).Result;
            return View("Categories/CategoriesTable.cshtml", Model);
        }
    }
}
