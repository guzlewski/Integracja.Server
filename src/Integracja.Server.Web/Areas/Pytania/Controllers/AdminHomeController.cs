using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Pytania.Models.AdminHome;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            Model.Questions = (List<QuestionDto>)await QuestionService.GetAll<QuestionDto>(UserId);
            Model.Alerts = GetAlerts();
            return View("AdminHome", Model);
        }

        public Task<IActionResult> GotoQuestionRead(int? id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionReadView), AdminQuestionController.Name, new { questionId = id }));
        }

        public Task<IActionResult> GotoQuestionDelete(int? id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionDelete), AdminQuestionController.Name, new { questionId = id }));
        }

        public Task<IActionResult> GotoQuestionCreate(int? id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), AdminQuestionController.Name, new { questionId = id }));
        }

        public Task<IActionResult> GotoQuestionUpdate(int? id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), AdminQuestionController.Name, new { questionId = id }));
        }
    }
}
