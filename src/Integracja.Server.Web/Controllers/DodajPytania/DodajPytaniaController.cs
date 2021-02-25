using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.DodajPytania;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers.DodajPytania
{

    public class DodajPytaniaController : ApplicationController, DodajPytaniaViewModel.IActions, QuestionViewModel.IActions
    {
        private DodajPytaniaViewModel Model { get; set; }

        public DodajPytaniaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new DodajPytaniaViewModel();
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

            Model.Categories = CategoryService.GetAll(UserId).Result;
            return View("~/Views/DodajPytania/Index.cshtml", Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnswerField(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (id.HasValue)
                question.CategoryId = id.Value;

            question.AddAnswer();

            SaveForm(question);

            return RedirectToAction("Index", "DodajPytania", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAnswerField(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (id.HasValue)
                question.CategoryId = id.Value;

            question.RemoveAnswer();

            SaveForm(question);

            return RedirectToAction("Index", "DodajPytania", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryRead(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            SaveForm(question);

            return RedirectToAction("Index", "DodajPytania", new { id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessQuestionForm(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (id.HasValue)
                question.CategoryId = id.Value;

            await QuestionService.Add(question.ToQuestionAdd(), UserId);

            return RedirectToAction("Index", "DodajPytania", new { id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(DodajPytaniaViewModel.NewCategory))] CategoryModel newCategory)
        {
             var mapper = Mappers.WebAutoMapper.Initialize();

            CategoryAdd categoryAdd = mapper.Map<CategoryAdd>(newCategory);

            int categoryId = await CategoryService.Add(categoryAdd, UserId);

            return RedirectToAction("Index", "DodajPytania", new { id = categoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public void SaveQuestionForm(QuestionModel question)
        {
            SaveForm<QuestionModel>(question);
            return;
        }
    }
}
