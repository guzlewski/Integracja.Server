using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Kategorie.Models.AdminHome;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Controllers
{
    [Area("Kategorie")]
    public class AdminHomeController : ApplicationController, IAdminHomeActions
    {
        public AdminHomeViewModel Model { get; set; }
        public static new string Name { get => "AdminHome"; }

        public AdminHomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int? id)
        {
            Model = new AdminHomeViewModel();
            Model.Categories = (System.Collections.Generic.List<Infrastructure.Models.CategoryDto>)CategoryService.GetAll(UserId).Result;

            if (id.HasValue) // forma kategorii do wyświetlania i edycji jest wbudowana w widok
            {
                var category = await CategoryService.Get(id.Value, UserId);
                Model.CategoryFormModel.Category = CategoryModel.ConvertToCategoryModel(category);
                Model.CategoryFormModel.ViewMode = Web.Models.Shared.Enums.ViewMode.Updating;
            }
            
            return View("AdminHome", Model);
        }

        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (id.HasValue)
                await CategoryService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CategoryUpdate(CategoryModel category)
        {
            await CategoryService.Update(category.Id, category.ToCategoryModify(), UserId);
            return RedirectToAction("Index", new { id = category.Id });
        }

        public async Task<IActionResult> CategoryCreate(CategoryModel category)
        {
            int categoryId = await CategoryService.Add(category.ToCategoryAdd(), UserId);
            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> CategoryRead(int? id)
        {
            return RedirectToAction("Index", new { id = id });
        }
    }
}
