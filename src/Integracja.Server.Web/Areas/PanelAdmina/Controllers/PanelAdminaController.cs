using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Models.PanelAdmina;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers.PanelAdmina
{
    [Area("PanelAdmina")]
    public class PanelAdminaController : ApplicationController, QuestionViewModel.IActions
    {
        private PanelAdminaViewModel Model { get; set; }

        public PanelAdminaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new PanelAdminaViewModel();
        }

        public IActionResult Index()
        {
            Model.Questions = QuestionService.GetAll(UserId).Result;
            return View("Index",Model);
        }

        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            await CategoryService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> QuestionUpdate(int? id)
        {
            QuestionViewModel viewModel = new QuestionViewModel("Pytanie", true, "PanelAdmina");

            var question = await QuestionService.Get(id.Value, UserId);

            var mapper = Mappers.WebAutoMapper.Initialize();

            viewModel.Question = mapper.Map<QuestionModel>(question);

            return View("~/Views/Shared/_Question.cshtml", viewModel);
        }

        public async Task<IActionResult> QuestionDelete(int? id)
        {
            if (id.HasValue)
                await QuestionService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddAnswerField(int? id, [Bind(Prefix = "Question")] QuestionModel question)
        {
            question.AddAnswer();

            QuestionViewModel viewModel = new QuestionViewModel("Pytanie", true, "PanelAdmina");

            viewModel.Question = question;

            return View("~/Views/Shared/_Question.cshtml", viewModel);
        }

        public async Task<IActionResult> RemoveAnswerField(int? id, [Bind( Prefix = "Question")] QuestionModel question)
        {
            question.RemoveAnswer();

            QuestionViewModel viewModel = new QuestionViewModel("Pytanie", true, "PanelAdmina");

            viewModel.Question = question;

            return View("~/Views/Shared/_Question.cshtml", viewModel);
        }

        public async Task<IActionResult> ProcessQuestionForm(int? id, [Bind( Prefix = "Question")] QuestionModel question)
        {
            if (id.HasValue)
                question.CategoryId = id.Value;

            await QuestionService.Add(question.ToQuestionAdd() , UserId);

            return RedirectToAction("Index");
        }
    }
}
