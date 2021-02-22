using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Models.PanelAdmina;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers.PanelAdmina
{
    public class PanelAdminaController : ApplicationController
    {
        private PanelAdminaViewModel Model { get; set; }

        public PanelAdminaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new PanelAdminaViewModel();
        }

        public IActionResult Index()
        {
            Model.Categories = CategoryService.GetAll(UserId).Result;
            Model.Questions = QuestionService.GetAll(UserId).Result;
            return View(Model);
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
            /*QuestionViewModel viewModel = new QuestionViewModel("Pytanie", false, "");
            var mapper = AutoMapperConfig.Initialize();
            var question = await QuestionService.Get(id.Value, UserId);
            viewModel.Question = mapper.Map<QuestionAdd>(question);*/
            return View("~/Views/Shared/_Question.cshtml");
        }

        public async Task<IActionResult> QuestionDelete(int? id)
        {
            if (id.HasValue)
                await QuestionService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }
    }
}
