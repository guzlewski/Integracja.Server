using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Mappers;
using Integracja.Server.Infrastructure.Repositories;
using Integracja.Server.Infrastructure.Services;
using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers
{
    public class PanelAdminaController : ApplicationController
    {
        // services
        private ICategoryService _categoryService;
        private ICategoryService CategoryService { get => _categoryService; }
        private IQuestionService _questionService;
        private IQuestionService QuestionService { get => _questionService; }
        // model 
        private PanelAdminaViewModel _model;
        private PanelAdminaViewModel Model { get => _model; }

        public PanelAdminaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            _categoryService = new CategoryService(new CategoryRepository(dbContext), AutoMapperConfig.Initialize());
            _questionService = new QuestionService(new QuestionRepository(dbContext), AutoMapperConfig.Initialize());
            _model = new PanelAdminaViewModel();
        }

        public IActionResult Index()
        {
            Model.Categories = CategoryService.GetAll(UserId).Result;
            Model.Questions = QuestionService.GetAll(UserId).Result;
            return View(Model);
        }

        public async Task<IActionResult> CategoryDelete( int? id )
        {
            if (!id.HasValue)
                return NotFound();

            await CategoryService.Delete(id.Value, UserId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> QuestionUpdate( int? id )
        {
            QuestionViewModel viewModel = new QuestionViewModel("Pytanie", false, "");
            var mapper = AutoMapperConfig.Initialize();
            var question = await QuestionService.Get(id.Value, UserId);
            viewModel.Question = mapper.Map<QuestionAdd>(question);
            return View("~/Views/Shared/_Question.cshtml", viewModel );
        }
    }
}
