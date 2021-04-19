using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
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
    public class QuestionController : ApplicationController, IQuestionActions
    {
        protected QuestionViewModel Model { get; set; }
        protected virtual string QuestionViewName => "Question";
        public static new string Name { get => "Question"; }
        public QuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public virtual IActionResult Index()
        {
            return IndexResult("Index", "Home");
        }

        protected IActionResult IndexResult(string redirectActionName, string redirectControllerName)
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
                return RedirectToAction(redirectActionName, redirectControllerName);
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
                return RedirectToAction("Index");
            }
            // jeśli inaczej to zostajemy i można dodać kolejne pytanie do kategorii
            else
            {
                alerts.Add(new AlertModel(AlertType.Info, "Możesz teraz ponownie utworzyć pytanie dla wybranej kategorii."));
                SetAlerts(alerts);
                return RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep2), new { categoryId = question.CategoryId });
            }

        }
        public async Task<IActionResult> QuestionReadView(int questionId)
        {
            QuestionModel q = await QuestionService.Get<QuestionModel>(questionId, UserId);

            return View("QuestionCard", q);
        }
        public async Task<IActionResult> QuestionUpdate(QuestionModel question)
        {
            int questionId = await QuestionService.Update(question.Id.Value, Mapper.Map<EditQuestionDto>(question), UserId);

            SetAlert(QuestionAlert.UpdateSuccess());

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> QuestionUpdateView(int questionId)
        {
            var question = await QuestionService.Get<QuestionModel>(questionId, UserId);
            SaveToTempData(question);
            return RedirectToAction("Index");
        }
        public virtual async Task<IActionResult> QuestionDelete(int questionId)
        {
            return await QuestionDeleteResult(questionId, "Index", HomeController.Name);
        }

        protected async Task<IActionResult> QuestionDeleteResult(int? questionId, string redirectActionName, string redirectControllerName)
        {
            if (questionId.HasValue)
                await QuestionService.Delete(questionId.Value, UserId);

            SetAlert(QuestionAlert.DeleteSuccess());

            return RedirectToAction(redirectActionName, redirectControllerName);
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

        public Task<IActionResult> GotoQuestionDelete(int questionId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IQuestionActions.QuestionDelete), new { questionId = questionId }));
        }

        public Task<IActionResult> GotoHome()
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", HomeController.Name));
        }
    }
}
