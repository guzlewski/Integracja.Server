using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Kategorie.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class QuestionController : ApplicationController, IQuestionActions
    {
        protected QuestionViewModel Model { get; set; }

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
            var alert = GetAlert<QuestionAlert>();
            var question = TryRetrieveFromTempData<QuestionModel>();

            if (question != null)
            {
                Model = new QuestionViewModel(question, alert);
                return View("Question", Model);
            }
            else
            {
                SetAlert(alert); // przekazuję dalej
                return RedirectToAction(redirectActionName, redirectControllerName);
            }
        }

        public async Task<IActionResult> QuestionCreateViewStep1()
        {
            return RedirectToAction("Index", CategorySelectController.Name, new { area = "Kategorie" });
        }

        public async Task<IActionResult> QuestionCreateViewStep2(int categoryId)
        {
            Model = new QuestionViewModel(ViewMode.Creating);
            // mogłem właśnie dodać pytanie i trafić tutaj ponownie więc wyświetlam alert
            Model.Alert = GetAlert<QuestionAlert>();

            Model.Question.CategoryId = categoryId;

            return View("Question", Model);
        }
        public async Task<IActionResult> QuestionCreate(QuestionModel question)
        {
            int questionId = await QuestionService.Add(question.ToQuestionAdd(), UserId);

            SetAlert(QuestionAlert.QuestionCreateSuccess());

            // jeśli weszło z edycji to cofamy do głównego panelu 
            if (question.Id.HasValue)
                return RedirectToAction("Index");
            // jeśli inaczej to zostajemy i można dodać kolejne pytanie do kategorii
            else return RedirectToAction( nameof(IQuestionActions.QuestionCreateViewStep2), new { categoryId = question.CategoryId });
        }
        public async Task<IActionResult> QuestionReadView(int? questionId)
        {
            Model = new QuestionViewModel();
            if (questionId.HasValue)
                Model.Question = (QuestionModel)await QuestionService.Get(questionId.Value, UserId);
            Model.ViewMode = ViewMode.Reading;
            return View("Question", Model);
        }
        public async Task<IActionResult> QuestionUpdate(QuestionModel question)
        {
            int questionId = await QuestionService.Update( question.Id.Value, question.ToQuestionModify(), UserId );

            SetAlert(QuestionAlert.QuestionUpdateSuccess());

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> QuestionUpdateView(int? questionId)
        {
            Model = new QuestionViewModel();

            if (questionId.HasValue)
            {
                Model.Question = (QuestionModel)await QuestionService.Get(questionId.Value, UserId);
                var q = await QuestionService.Get(questionId.Value, UserId);
            }
                
            Model.ViewMode = ViewMode.Updating;
            return View("Question", Model);
        }
        public async Task<IActionResult> QuestionDelete(int? questionId)
        {
            if (questionId.HasValue)
                await QuestionService.Delete(questionId.Value, UserId);

            SetAlert(QuestionAlert.QuestionDeleteSuccess());

            return RedirectToAction("Index", HomeController.Name);
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
    }
}
