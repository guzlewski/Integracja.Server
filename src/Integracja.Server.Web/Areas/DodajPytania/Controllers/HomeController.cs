using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Areas.DodajPytania.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Controllers
{
    [Area("DodajPytania")]
    public class HomeController : ApplicationController, HomeViewModel.IActions, QuestionViewModel.IActions
    {
        private HomeViewModel Model { get; set; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new HomeViewModel();
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            return RedirectToAction("Category", new { id });
        }

        [HttpGet]
        [ActionName("Category")]
        public IActionResult Category(int? id)
        {
            QuestionModel savedForm = TryRetrieveForm<QuestionModel>();

            if (savedForm != default(QuestionModel))
            {
                Model.QuestionViewModel.Question = savedForm;
            }

            // gdyby nie było kategorii z zapisanej formy ?
            if (id.HasValue && !Model.QuestionViewModel.Question.CategoryId.HasValue)
                Model.QuestionViewModel.Question.CategoryId = id.Value;

            Model.Categories = CategoryModel.ToList(CategoryService.GetAll(UserId).Result);
            return View("Index",Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnswerField(
            int? categoryId,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.AddAnswer();

            SaveForm(question);

            return RedirectToAction("Index", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAnswerField(
            int? categoryId,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.RemoveAnswer();

            SaveForm(question);

            return RedirectToAction("Index", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessQuestionForm(
            int? categoryId,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            var q = question.ToQuestionAdd();

            await QuestionService.Add(q, UserId);

            return RedirectToAction("Index", new { categoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(HomeViewModel.Category))] CategoryModel newCategory)
        {
             var mapper = Mappers.WebAutoMapper.Initialize();

            CategoryAdd categoryAdd = mapper.Map<CategoryAdd>(newCategory);

            int categoryId = await CategoryService.Add(categoryAdd, UserId);

            return RedirectToAction("Index", "Home", new { id = categoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryRead(
            [Bind(Prefix = nameof(HomeViewModel.Category))] CategoryModel category)
        {
            return RedirectToAction("Index", new { id = category.Id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public void SaveQuestionForm(QuestionModel question)
        {
            SaveForm<QuestionModel>(question);
            return;
        }
    }
}
