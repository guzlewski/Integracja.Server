using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Questions;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    [Area("PanelAdmina")]
    public class QuestionsController : ApplicationController, QuestionsViewModel.IActions
    {
        public QuestionsViewModel Model { get; set; }

        public QuestionsController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new QuestionsViewModel();
        }

        public async Task<IActionResult> Index( int? id )
        {
            if( id.HasValue )
            {
                QuestionModel savedForm = TryRetrieveFromTempData<QuestionModel>();
                if (savedForm != default(QuestionModel)) // ==/!= not implemented ? 
                {
                    Model.QuestionViewModel.Question = savedForm;
                }
                else Model.QuestionViewModel.Question = (QuestionModel)await QuestionService.Get(id.Value, UserId);
            }

            Model.Questions = (System.Collections.Generic.List<Infrastructure.DTO.QuestionGetAll>)await QuestionService.GetAll(UserId);
            return View("Questions", Model);
        }

        public async Task<IActionResult> QuestionRead(int? id)
        {
            return RedirectToAction("Index", new { id = id });
        }

        public async Task<IActionResult> QuestionDelete(int? id)
        {
            if (id.HasValue)
                await QuestionService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddAnswerField(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.AddAnswer();

            SaveToTempData(question);

            return RedirectToAction("Index", new { id = question.Id });
        }

        public async Task<IActionResult> RemoveAnswerField(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.RemoveAnswer();

            SaveToTempData(question);

            return RedirectToAction("Index", new { id = question.Id });
        }

        public async Task<IActionResult> QuestionCreate(int? categoryId, QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            var q = question.ToQuestionAdd();
            q.IsPublic = true; // dodane z panelu admina więc publiczne

            int questionId = await QuestionService.Add(q, UserId);

            return RedirectToAction("Index", new { id = questionId });
        }

        public async Task<IActionResult> QuestionUpdate(int? categoryId, QuestionModel question)
        {
            var q = question.ToQuestionModify();

            if( question.Id.HasValue) // zawsze powinien mieć ale ¯\_(ツ)_/¯
                await QuestionService.Update( question.Id.Value, q, UserId);

            return RedirectToAction("Index", new { question.Id });
        }

        public async Task<IActionResult> QuestionReadToModal( int? id )
        {
            QuestionViewModel viewModel = new QuestionViewModel();
            viewModel.Question = (QuestionModel)await QuestionService.Get(id.Value, UserId);
            return PartialView("~/Views/Shared/Question/_Question.cshtml", viewModel);
        }
    }
}
