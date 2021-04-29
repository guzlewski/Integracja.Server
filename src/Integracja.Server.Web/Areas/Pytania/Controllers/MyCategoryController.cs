using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Kategorie.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.MyCategory;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class MyCategoryController : ApplicationController, IMyCategoryActions, IHomeNav
    {
        public static new string Name => "MyCategory";

        public MyCategoryController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int categoryId )
        {
            MyCategoryViewModel model = new();
            model.Alerts = GetAlerts();
            model.Questions = (List<QuestionModel>)await QuestionService.GetOwned<QuestionModel>(categoryId, UserId);
            return View("MyCategory", model);
        }

        public Task<IActionResult> GotoQuestionRead(int questionId)
        {

            return Task.FromResult<IActionResult>(RedirectToAction(nameof(MyCategoryQuestionController.QuestionReadView), MyCategoryQuestionController.Name, new { questionId }));
        }

        public Task<IActionResult> GotoQuestionUpdate(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(MyCategoryQuestionController.QuestionUpdateView), MyCategoryQuestionController.Name, new { questionId }));
        }

        public Task<IActionResult> GotoQuestionDelete(int questionId, int categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(MyCategoryQuestionController.QuestionDelete), MyCategoryQuestionController.Name, new { questionId, categoryId}));
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

        public Task<IActionResult> GotoQuestionCreate()
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), MyCategoryQuestionController.Name));
        }
    }
}
