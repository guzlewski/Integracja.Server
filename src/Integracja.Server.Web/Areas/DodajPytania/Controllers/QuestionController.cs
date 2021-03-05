using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.DodajPytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Controllers
{
    [Area("DodajPytania")]
    public class QuestionController : ApplicationController, IQuestionActions
    {
        private QuestionViewModel Model { get; set; }
        public static new string Name { get => "Question"; }

        public QuestionController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
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

        public Task<IActionResult> AddAnswerField(int? categoryId, QuestionModel question)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> RemoveAnswerField(int? categoryId, QuestionModel question)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> QuestionCreate(int? categoryId, QuestionModel question)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> QuestionUpdate(int? categoryId, QuestionModel question)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> QuestionDelete(int? id)
        {
            if (id.HasValue)
                await QuestionService.Delete(id.Value, UserId);
            return RedirectToAction("Index", HomeController.Name);
        }

    }
}
