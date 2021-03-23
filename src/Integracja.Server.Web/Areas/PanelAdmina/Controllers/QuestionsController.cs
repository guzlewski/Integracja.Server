﻿using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Question;
using Integracja.Server.Web.Areas.PanelAdmina.Models.Questions;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    [Area("PanelAdmina")]
    public class QuestionsController : ApplicationController, IQuestionsActions
    {
        public QuestionsViewModel Model { get; set; }

        public QuestionsController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
            Model = new QuestionsViewModel();
        }

        public async Task<IActionResult> Index( int? id )
        {
            Model.Questions = (System.Collections.Generic.List<Infrastructure.Models.QuestionDto>)await QuestionService.GetAll(UserId);
            Model.Alert = GetAlert<QuestionAlert>();
            return View("Questions", Model);
        }

        public async Task<IActionResult> GotoQuestionRead(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionReadView, QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionDelete(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionDelete, QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionCreate(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionCreateViewStep1, QuestionController.Name, new { questionId = id });
        }

        public async Task<IActionResult> GotoQuestionUpdate(int? id)
        {
            return RedirectToAction(IQuestionActions.NameOfQuestionUpdateView, QuestionController.Name, new { questionId = id } );
        }
    }
}