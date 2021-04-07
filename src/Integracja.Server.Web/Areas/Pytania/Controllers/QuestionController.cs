using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Enums;
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

        protected IActionResult IndexResult( string redirectActionName, string redirectControllerName )
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

        public async Task<IActionResult> QuestionCreateViewStep1(int? categoryId)
        {
            return RedirectToAction("Index", CategoryForQuestionController.Name, new { area = "Kategorie", id = categoryId });
        }

        public async Task<IActionResult> QuestionCreateViewStep2(int categoryId)
        {
            Model = new QuestionViewModel(ViewMode.Creating);
            // mogłem właśnie dodać pytanie i trafić tutaj ponownie więc wyświetlam alert
            Model.Alerts = GetAlerts();

            Model.Form.Question.CategoryId = categoryId;

            var tmp = await CategoryService.Get<CategoryModel>(categoryId, UserId);
            Model.Form.Question.CategoryName = tmp.Name;

            return View(QuestionViewName, Model);
        }
        public async Task<IActionResult> QuestionCreate(QuestionModel question)
        {
            int questionId = await QuestionService.Add( Mapper.Map<CreateQuestionDto>(question), UserId);

            List<AlertModel> alerts = new List<AlertModel>();
            alerts.Add(QuestionAlert.CreateSuccess());
            


            // jeśli weszło z edycji to cofamy do głównego panelu 
            if (question.Id.HasValue)
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
            int questionId = await QuestionService.Update( question.Id.Value, Mapper.Map<EditQuestionDto>(question), UserId );

            SetAlert(QuestionAlert.UpdateSuccess());

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> QuestionUpdateView(int? questionId)
        {
            Model = new QuestionViewModel();

            if (questionId.HasValue)
            {
                Model.Form.Question = await QuestionService.Get<QuestionModel>(questionId.Value, UserId);
            }
                
            Model.Form.ViewMode = ViewMode.Updating;
            return View(QuestionViewName, Model);
        }
        public virtual async Task<IActionResult> QuestionDelete(int? questionId)
        {
            return await QuestionDeleteResult( questionId, "Index", HomeController.Name);
        }

        protected async Task<IActionResult> QuestionDeleteResult(int? questionId, string redirectActionName, string redirectControllerName)
        {
            if (questionId.HasValue)
                await QuestionService.Delete(questionId.Value, UserId);

            SetAlert(QuestionAlert.DeleteSuccess());

            return RedirectToAction(redirectActionName, redirectControllerName);
        }
        
        public async Task<IActionResult> AddAnswerField(QuestionModel question)
        {
            question.AddAnswer();

            SaveToTempData(question);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAnswerField(QuestionModel question)
        {
            question.RemoveAnswer();

            SaveToTempData(question);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> QuestionCreateCategoryUpdate(int categoryId)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep1), new { categoryId = categoryId });
        }

        public async Task<IActionResult> GotoQuestionUpdate(int questionId)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionUpdateView), new { questionId = questionId });
        }

        public async Task<IActionResult> GotoQuestionDelete(int questionId)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionDelete), new { questionId = questionId });
        }

        public async Task<IActionResult> GotoHome()
        {
            return RedirectToAction("Index", HomeController.Name);
        }
    }
}
