using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class HomeQuestionController : ApplicationController, IQuestionActions
    {
        protected QuestionViewModel Model { get; set; }
        protected virtual string QuestionViewName => "Question";
        public new const string Name = "HomeQuestion";

        public HomeQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public virtual IActionResult Index(int categoryId)
        {
            return IndexResult("Index", "Home", new { categoryId });
        }

        protected IActionResult IndexResult(string redirectActionName, string redirectControllerName, object routeValues = null)
        {
            var alert = GetAlerts();
            var question = TryRetrieveFromTempData<QuestionModel>();

            if (question != null)
            {
                Model = new QuestionViewModel(question, alert);
                return View(QuestionViewName, Model);
            }
            else
            {
                SetAlerts(alert); // przekazuję dalej
                return RedirectToAction(redirectActionName, redirectControllerName, routeValues);
            }
        }

        public Task<IActionResult> QuestionCreateViewStep1(int? categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", CategoryForQuestionController.Name, new { area = "Kategorie", id = categoryId }));
        }

        public async Task<IActionResult> QuestionCreateViewStep2(int categoryId)
        {
            var question = new QuestionModel();

            var category = await CategoryService.Get<CategoryModel>(categoryId, UserId);
            question.CategoryName = category.Name;
            question.CategoryId = categoryId;

            SaveToTempData(question);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> QuestionCreate(QuestionModel question)
        {
            int questionId = await QuestionService.Add(Mapper.Map<CreateQuestionDto>(question), UserId);

            List<AlertModel> alerts = new List<AlertModel>();
            alerts.Add(QuestionAlert.CreateSuccess());

            // jeśli weszło z edycji to cofamy do głównego panelu 
            if (question.IsPersisted)
            {
                SetAlerts(alerts);
                return RedirectToAction("Index", new { categoryId = question.CategoryId });
            }
            // jeśli inaczej to zostajemy i można dodać kolejne pytanie do kategorii
            else
            {
                alerts.Add(new AlertModel(AlertType.Info, "Możesz teraz ponownie utworzyć pytanie dla wybranej kategorii."));
                SetAlerts(alerts);
                return RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep2), new { categoryId = question.CategoryId });
            }

        }

        public async Task<IActionResult> QuestionReadView(int questionId )
        {
            QuestionDetailsViewModel model = new();
            model.Question = await QuestionService.Get<QuestionModel>(questionId, UserId);
            return View("QuestionCard", model);
        }
        public async Task<IActionResult> QuestionUpdate(QuestionModel question)
        {
            int questionId = await QuestionService.Update(question.Id.Value, Mapper.Map<EditQuestionDto>(question), UserId);

            SetAlert(QuestionAlert.UpdateSuccess());

            return RedirectToAction("Index", new { categoryId = question.CategoryId });
        }
        public async Task<IActionResult> QuestionUpdateView(int questionId)
        {
            var question = await QuestionService.Get<QuestionModel>(questionId, UserId);
            SaveToTempData(question);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> QuestionDelete(int questionId, int categoryId)
        {
            await QuestionService.Delete(questionId, UserId);

            SetAlert(QuestionAlert.DeleteSuccess());

            return RedirectToAction("Index", new { categoryId });
        }

        public Task<IActionResult> AddAnswerField(QuestionModel question)
        {
            question.AddAnswer();

            SaveToTempData(question);

            return Task.FromResult<IActionResult>(RedirectToAction("Index"));
        }
        public Task<IActionResult> RemoveAnswerField(QuestionModel question)
        {
            question.RemoveAnswer();

            SaveToTempData(question);

            return Task.FromResult<IActionResult>(RedirectToAction("Index"));
        }

        public Task<IActionResult> QuestionCreateCategoryUpdate(int categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), new { categoryId = categoryId }));
        }

        public Task<IActionResult> GotoQuestionUpdate(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), new { questionId = questionId }));
        }

        public Task<IActionResult> GotoQuestionDelete(int questionId, int categoryId )
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionDelete), new { questionId = questionId, categoryId = categoryId }));
        }
    }
}
