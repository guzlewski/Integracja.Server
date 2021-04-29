using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Pytania.Models.Home;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class AllQuestionsController : HomeController
    {
        public static new string Name { get => "AllQuestions"; }
        public override string RedirectController => AllQuestionController.Name;

        public AllQuestionsController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        override public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Alerts = GetAlerts();
            model.Questions = (List<QuestionModel>)await QuestionService.GetAll<QuestionModel>(UserId);
            model.Title = "Wszystkie pytania";
            return View(IndexViewPath, model);
        }
    }
}
