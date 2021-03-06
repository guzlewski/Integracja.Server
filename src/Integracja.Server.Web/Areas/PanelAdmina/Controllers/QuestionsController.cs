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
    public class QuestionsController : ApplicationController, IQuestionsActions
    {
        public QuestionsViewModel Model { get; set; }

        public QuestionsController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new QuestionsViewModel();
        }

        public async Task<IActionResult> Index( int? id )
        {
            Model.Questions = (System.Collections.Generic.List<Infrastructure.DTO.QuestionGetAll>)await QuestionService.GetAll(UserId);
            return View("Questions", Model);
        }

        public async Task<IActionResult> GotoQuestionRead(int? id)
        {
            return RedirectToAction("Index", new { id = id });
        }

        public async Task<IActionResult> GotoQuestionDelete(int? id)
        {
            if (id.HasValue)
                await QuestionService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }

        public Task<IActionResult> GotoQuestionCreate(int? id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> GotoQuestionUpdate(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
