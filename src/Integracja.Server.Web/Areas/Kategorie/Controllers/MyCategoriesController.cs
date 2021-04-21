using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Models.MyCategories;
using Integracja.Server.Web.Areas.Pytania.Controllers;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Controllers
{
    [Area("Kategorie")]
    public class MyCategoriesController : ApplicationController, IMyCategoriesActions
    {
        public static new string Name { get => "MyCategories"; }

        public MyCategoriesController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index()
        {
            MyCategoriesViewModel model = new();
            model.Categories = (System.Collections.Generic.List<CategoryModel>)await CategoryService.GetOwned<CategoryModel>(UserId);
            return View("MyCategories", model);
        }

        public Task<IActionResult> GotoCategoryRead(int id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index",MyCategoryController.Name, new { categoryId = id, area = "Pytania" }));
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

        public Task<IActionResult> GotoCategoryUpdate(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> CategoryUpdate(CategoryModel category) 
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> MyQuestions()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(MyQuestions), Pytania.Controllers.HomeController.Name, new { area = "Pytania" }));
        }

        public Task<IActionResult> AllQuestions()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(AllQuestions), Pytania.Controllers.HomeController.Name, new { area = "Pytania" }));
        }

        public Task<IActionResult> MyCategories()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(MyCategories), Pytania.Controllers.HomeController.Name, new { area = "Pytania" }));
        }

        public Task<IActionResult> GotoQuestionCreate()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(GotoQuestionCreate), Pytania.Controllers.HomeController.Name, new { area = "Pytania" }));
        }
    }
}
