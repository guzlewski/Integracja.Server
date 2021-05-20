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
    public class MyQuestionsController : HomeController
    {
        public new const string Name = "MyQuestions";
        public override string RedirectController => MyQuestionController.Name;

        public MyQuestionsController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        override public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Alerts = GetAlerts();
            model.QuestionTable.Questions = (List<QuestionModel>)await QuestionService.GetOwned<QuestionModel>(UserId);
            model.Title = "Moje pytania";
            return View(IndexViewPath, model);
        }
    }
}
