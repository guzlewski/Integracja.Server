using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Pytania.Models.Home;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class HomeController : ApplicationController, IHomeActions
    {
        private HomeViewModel Model { get; set; }
        public static new string Name { get => "Home"; } 

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new HomeViewModel();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            Model.Alert = GetAlert<QuestionAlert>();

            Model.Questions = QuestionModel.ConvertToList( await QuestionService.GetAll(UserId) );
            return View(Model);
        }

        public FileContentResult Picture()
        {
            var user = UserManager.GetUserAsync(User);

            return new FileContentResult(user.Result.Picture, "image/jpeg");
        }

        public async Task<IActionResult> GotoQuestionCreate()
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionCreateViewStep1, QuestionController.Name);
        }

        public async Task<IActionResult> GotoQuestionRead(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionReadView, QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionUpdate(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionUpdateView, QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionDelete(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionDelete, QuestionController.Name, new { questionId = id } );
        }
    }
}
