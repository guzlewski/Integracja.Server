using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Pytania.Models.Home;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Mapper;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class HomeController : ApplicationController, IHomeActions
    {
        private HomeViewModel Model { get; set; }
        public static new string Name { get => "Home"; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
            Model = new HomeViewModel();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            Model.Alerts = GetAlerts();
            Model.Questions = WebAutoMapper.Initialize().Map<List<QuestionModel>>( await QuestionService.GetAll<QuestionDto>(UserId) );
            return View(Model);
        }

        public async Task<IActionResult> GotoQuestionCreate()
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), QuestionController.Name);
        }

        public async Task<IActionResult> GotoQuestionRead(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionReadView), QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionUpdate(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionDelete(int? id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionDelete), QuestionController.Name, new { questionId = id } );
        }
    }
}
