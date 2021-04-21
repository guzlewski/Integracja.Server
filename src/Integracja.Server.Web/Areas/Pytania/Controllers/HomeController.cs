using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Kategorie.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Home;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class HomeController : ApplicationController, IHomeActions
    {
        public static new string Name { get => "Home"; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        [HttpGet]
        public Task<IActionResult> Index()
        {
            return Task.FromResult < IActionResult >(RedirectToAction(nameof(IHomeActions.GotoOwnedQuestions)));
        }

        public Task<IActionResult> GotoQuestionCreate()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), HomeQuestionController.Name));
        }

        public Task<IActionResult> GotoQuestionRead(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionReadView), HomeQuestionController.Name, new { questionId }));
        }

        public Task<IActionResult> GotoQuestionUpdate(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), HomeQuestionController.Name, new { questionId }));
        }

        public Task<IActionResult> GotoQuestionDelete(int questionId, int categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionDelete), HomeQuestionController.Name, new { questionId }));
        }

        public async Task<IActionResult> GotoOwnedQuestions()
        {
            HomeViewModel model = new HomeViewModel();
            model.Alerts = GetAlerts();
            model.Questions = (List<QuestionModel>)await QuestionService.GetOwned<QuestionModel>(UserId);
            model.Title = "Moje pytania";
            return View("Index", model);
        }

        public async Task<IActionResult> GotoAllQuestions()
        {
            HomeViewModel model = new HomeViewModel();
            model.Alerts = GetAlerts();
            model.Questions = (List<QuestionModel>)await QuestionService.GetAll<QuestionModel>(UserId);
            model.Title = "Wszystkie pytania";
            return View("Index", model);
        }

        public Task<IActionResult> GotoOwnedCategories()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", OwnedCategoriesController.Name, new { area = "Kategorie" }));
        }
    }
}
