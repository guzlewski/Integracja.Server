using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Models.OwnedCategories;
using Integracja.Server.Web.Areas.Pytania.Controllers;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Controllers
{
    [Area("Kategorie")]
    public class OwnedCategoriesController : ApplicationController, IOwnedCategoriesActions
    {
        public static new string Name { get => "OwnedCategories"; }

        public OwnedCategoriesController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index()
        {
            OwnedCategoriesViewModel model = new();
            model.Categories = (System.Collections.Generic.List<CategoryModel>)await CategoryService.GetOwned<CategoryModel>(UserId);
            return View("OwnedCategories", model);
        }

        public Task<IActionResult> GotoCategoryRead(int id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index",OwnedCategoryQuestionsController.Name, new { categoryId = id, area = "Pytania" }));
        }

        public Task<IActionResult> GotoCategoryUpdate(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> GotoCategoryDelete(int id)
        {
            await CategoryService.Delete(id, UserId );
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CategoryCreate(CategoryModel category)
        {
            await CategoryService.Add(Mapper.Map<CreateCategoryDto>(category), UserId);
            return RedirectToAction("Index");
        }

        public Task<IActionResult> CategoryUpdate(CategoryModel category) 
        {
            throw new System.NotImplementedException();
        }
    }
}
