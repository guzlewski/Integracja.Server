using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Models.CategoryForQuestion;
using Integracja.Server.Web.Areas.Pytania.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Kategorie.Controllers
{
    [Area("Kategorie")]
    public class CategoryForQuestionController : ApplicationController, ICategoryForQuestionActions
    {
        public static new string Name { get => "CategoryForQuestion"; }

        public CategoryForQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int? id)
        {
            var model = new CategoryForQuestionViewModel();
            model.CategorySelectModel.Categories = (List<CategoryModel>)await CategoryService.GetAll<CategoryModel>(UserId);
            model.Alerts = GetAlerts();

            if (id.HasValue)
            {
                model.CategoryFormModel.Category.Id = id.Value;
            }

            return View("CategoryForQuestion", model);
        }

        public Task<IActionResult> CategoryRead(int id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", new { id = id }));
        }

        public async Task<IActionResult> CategoryCreate(CategoryModel category)
        {
            int categoryId = await CategoryService.Add(Mapper.Map<CreateCategoryDto>(category), UserId);
            return RedirectToAction("Index", new { id = categoryId });
        }

        public Task<IActionResult> GotoQuestionCreate(int? id)
        {
            if (id == null)
            {
                SetAlert(new AlertModel(AlertType.Warning, "Musisz wybrać lub utworzyć kategorię dla nowego pytania."));
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }
            else return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep2), HomeQuestionController.Name, new { area = "Pytania", categoryId = id }));
        }

        public Task<IActionResult> CategoryUpdate(CategoryModel category)
        {
            // nie daję możliwości zaaktualizowania kategorii stąd
            throw new System.NotImplementedException();
        }
    }
}
