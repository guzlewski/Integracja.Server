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

            question.Answers.Add(new AnswerModel());

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

            if (question.Answers.Count > 2)
                question.Answers.RemoveAt(question.Answers.Count - 1);

            SaveForm(question);

            return RedirectToAction("Index", "DodajPytania", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveQuestionForm(
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

            var mapper = Mappers.AutoMapperWebConfig.Initialize();

            QuestionAdd questionAdd = mapper.Map<QuestionAdd>(question);

            await QuestionService.Add(questionAdd, UserId);

            return RedirectToAction("Index", "DodajPytania", new { id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(DodajPytaniaViewModel.NewCategory))] CategoryAdd newCategory)
        {
            int categoryId = await CategoryService.Add(newCategory, UserId);
            return RedirectToAction("Index", "DodajPytania", new { id = categoryId });
        }

        public async Task<IActionResult> CategoryRead(int? categoryId)
        {
            return RedirectToAction("Index", "DodajPytania", new { id = categoryId });
        }
    }
}
