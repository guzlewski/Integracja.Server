using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.DodajPytania.Models.CategorySelect;
using Integracja.Server.Web.Areas.DodajPytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Controllers
{
    [Area("DodajPytania")]
    public class CategorySelectController : ApplicationController, ICategorySelectActions
    {
        private CategorySelectViewModel Model { get; set; }
        public CategorySelectController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
        }

        public static new string Name { get => "CategorySelect"; }
        public async Task<IActionResult> Index(int? id)
        {
            Model = new CategorySelectViewModel();
            Model.Categories = CategoryModel.ConvertToList(await CategoryService.GetAll(UserId));
            if (id.HasValue)
                Model.Category.Id = id.Value;
            return View("CategorySelect",Model);
        }

        public async Task<IActionResult> CategoryRead(int? id)
        {
            return RedirectToAction("Index", new { id = id });
        }

        public async Task<IActionResult> CategoryCreate(CategoryModel category)
        {
            int categoryId = await CategoryService.Add(category.ToCategoryAdd(), UserId);
            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> GotoQuestionCreate(int id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionCreateViewStep2, QuestionController.Name, new { categoryId = id });
        }
    }
}
