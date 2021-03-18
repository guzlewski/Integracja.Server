using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Pytania.Models.AdminHome;
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
    public class AdminHomeController : ApplicationController, IAdminHomeActions
    {
        public AdminHomeViewModel Model { get; set; }

        public new static string Name { get => "AdminHome"; }

        public AdminHomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
            Model = new AdminHomeViewModel();
        }

        public async Task<IActionResult> Index(int? id)
        {
            Model.Questions = (System.Collections.Generic.List<Infrastructure.Models.QuestionDto>)await QuestionService.GetAll(UserId);
            Model.Alerts = GetAlerts();
            return View("AdminHome", Model);
        }

        public async Task<IActionResult> GotoQuestionRead(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionReadView), AdminQuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionDelete(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionDelete), AdminQuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionCreate(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), AdminQuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionUpdate(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), AdminQuestionController.Name, new { questionId = id });
        }
    }
}
