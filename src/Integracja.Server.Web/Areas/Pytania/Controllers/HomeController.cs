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
        virtual public string RedirectController { get => HomeQuestionController.Name; }
        public const string IndexViewPath = "~/Areas/Pytania/Views/Home/Index.cshtml";

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        [HttpGet]
        virtual public Task<IActionResult> Index()
        {
            return Task.FromResult < IActionResult >(RedirectToAction(nameof(IHomeActions.MyQuestions)));
        }

        public Task<IActionResult> GotoQuestionCreate()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), RedirectController));
        }

        public Task<IActionResult> GotoQuestionRead(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionReadView), RedirectController, new { questionId }));
        }

        public Task<IActionResult> GotoQuestionUpdate(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), RedirectController, new { questionId }));
        }

        public Task<IActionResult> GotoQuestionDelete(int questionId, int categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionDelete), RedirectController, new { questionId }));
        }

        public Task<IActionResult> MyQuestions()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", MyQuestionsController.Name));
        }

        public Task<IActionResult> AllQuestions()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", AllQuestionsController.Name));
        }

        public Task<IActionResult> MyCategories()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", MyCategoriesController.Name, new { area = "Kategorie" }));
        }
    }
}
