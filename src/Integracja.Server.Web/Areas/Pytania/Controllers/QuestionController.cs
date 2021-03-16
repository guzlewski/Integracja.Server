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
            var alert = GetAlert<QuestionAlert>();
            var question = TryRetrieveFromTempData<QuestionModel>();

            if (question != null)
            {
                Model = new QuestionViewModel(question, alert);
                return View(QuestionViewName, Model);
            }
            else
            {
                SetAlert(alert); // przekazuję dalej
                return RedirectToAction(redirectActionName, redirectControllerName);
            }
        }

        public async Task<IActionResult> QuestionCreateViewStep1()
        {
            return RedirectToAction("Index", CategoryForQuestionController.Name, new { area = "Kategorie" });
        }

        public async Task<IActionResult> QuestionCreateViewStep2(int categoryId)
        {
            Model = new QuestionViewModel(ViewMode.Creating);
            // mogłem właśnie dodać pytanie i trafić tutaj ponownie więc wyświetlam alert
            Model.Alert = GetAlert<QuestionAlert>();

            Model.Form.Question.CategoryId = categoryId;

            return View(QuestionViewName, Model);
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
                Model.Form.Question = (QuestionModel)await QuestionService.Get(questionId.Value, UserId);
            Model.Form.ViewMode = ViewMode.Reading;
            return View(QuestionViewName, Model);
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
                Model.Form.Question = (QuestionModel)await QuestionService.Get(questionId.Value, UserId);
                var q = await QuestionService.Get(questionId.Value, UserId);
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

            SetAlert(QuestionAlert.QuestionDeleteSuccess());

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
    }
}
