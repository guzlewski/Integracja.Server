using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Pytania.Models.MyCategory;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class MyCategoryController : ApplicationController, IMyCategoryActions
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
    }
}
