using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Categories;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    [Area("PanelAdmina")]
    public class CategoriesController : ApplicationController, ICategoriesActions
    {
        private CategoriesViewModel Model { get; set; }

        public CategoriesController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
            Model = new CategoriesViewModel();
        }

        public async Task<IActionResult> Index( int? id )
        {
            if (id.HasValue)
            {
                var category = await CategoryService.Get(id.Value, UserId);
                Model.Category = CategoryModel.ConvertToCategoryModel(category);
                Model.ViewMode = Web.Models.Shared.Enums.ViewMode.Updating;
            }
            Model.Categories = (System.Collections.Generic.List<Infrastructure.Models.CategoryDto>)CategoryService.GetAll(UserId).Result;
            return View("Categories",Model);
        }

        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if( id.HasValue )
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
            return RedirectToAction("Index", new { id = id } );
        }
    }
}
