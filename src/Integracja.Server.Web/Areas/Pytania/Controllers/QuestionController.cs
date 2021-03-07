using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Areas.Pytania.Models.Home;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class QuestionController : ApplicationController, IQuestionActions
    {
        protected QuestionViewModel Model { get; set; }
        public static new string Name { get => "Question"; }

        public QuestionController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
        }

        public virtual IActionResult Index()
        {
            var question = TryRetrieveFromTempData<QuestionModel>();
            if (question != null)
            {
                Model = new QuestionViewModel(question);
                return View("Question", Model);
            }
            else return RedirectToAction("Index", HomeController.Name);
        }

        public async Task<IActionResult> QuestionCreateViewStep1()
        {
            return RedirectToAction("Index", CategorySelectController.Name);
        }

        public async Task<IActionResult> QuestionCreateViewStep2(int categoryId)
        {
            Model = new QuestionViewModel(ViewMode.Creating);
            Model.Question.CategoryId = categoryId;
            return View("Question", Model);
        }

        public async Task<IActionResult> QuestionReadView(int? id )
        {
            Model = new QuestionViewModel();
            if( id.HasValue )
                Model.Question = (QuestionModel)await QuestionService.Get(id.Value, UserId);
            Model.ViewMode = ViewMode.Reading;
            return View("Question", Model);
        }

        public async Task<IActionResult> QuestionUpdateView(int? id)
        {
            Model = new QuestionViewModel();
            if (id.HasValue)
                Model.Question = (QuestionModel)await QuestionService.Get(id.Value, UserId);
            Model.ViewMode = ViewMode.Updating;
            return View("Question", Model);
        }

        public async Task<IActionResult> AddAnswerField(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.AddAnswer();

            SaveToTempData(question);

            /*Model = new QuestionViewModel(question);*/

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveAnswerField(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.RemoveAnswer();

            SaveToTempData(question);

            /*Model = new QuestionViewModel(question);*/

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> QuestionCreate(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            int questionId = await QuestionService.Add(question.ToQuestionAdd(), UserId);

            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> QuestionUpdate(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            int questionId = await QuestionService.Update( question.Id.Value, question.ToQuestionModify(), UserId );

            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> QuestionDelete(int? id)
        {
            if (id.HasValue)
                await QuestionService.Delete(id.Value, UserId);
            return RedirectToAction("Index", HomeController.Name);
        }
    }
}
