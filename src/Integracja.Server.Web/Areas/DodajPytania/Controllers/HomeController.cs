﻿using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Areas.DodajPytania.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Controllers
{
    [Area("DodajPytania")]
    public class HomeController : ApplicationController, HomeViewModel.IActions
    {
        private HomeViewModel Model { get; set; }

        public HomeController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new HomeViewModel();
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            return RedirectToAction("Category", new { id });
        }

        [HttpGet]
        [ActionName("Category")]
        public IActionResult Category(int? id)
        {
            QuestionModel savedForm = TryRetrieveFromTempData<QuestionModel>();

            if (savedForm != default(QuestionModel))
            {
                Model.QuestionViewModel.Question = savedForm;
            }

            // gdyby nie było kategorii z zapisanej formy ?
            if (id.HasValue && !Model.QuestionViewModel.Question.CategoryId.HasValue)
                Model.QuestionViewModel.Question.CategoryId = id.Value;

            Model.Categories = CategoryModel.ConvertToList(CategoryService.GetAll(UserId).Result);
            return View("Index",Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnswerField(
            int? categoryId,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.AddAnswer();

            SaveToTempData(question);

            return RedirectToAction("Index", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAnswerField(
            int? categoryId,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            question.RemoveAnswer();

            SaveToTempData(question);

            return RedirectToAction("Index", new { id = question.CategoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionCreate(
            int? categoryId,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question)
        {
            if (categoryId.HasValue)
                question.CategoryId = categoryId.Value;

            await QuestionService.Add(question.ToQuestionAdd(), UserId);

            return RedirectToAction("Index", new { categoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(HomeViewModel.Category))] CategoryModel newCategory)
        {
            int categoryId = await CategoryService.Add(newCategory.ToCategoryAdd(), UserId);

            return RedirectToAction("Index", "Home", new { id = categoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryRead(
            [Bind(Prefix = nameof(HomeViewModel.Category))] CategoryModel category)
        {
            return RedirectToAction("Index", new { id = category.Id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public void SaveQuestionForm(QuestionModel question)
        {
            SaveToTempData<QuestionModel>(question);
            return;
        }

        public Task<IActionResult> QuestionUpdate(int? categoryId, QuestionModel question)
        {
            throw new System.NotImplementedException();
        }
    }
}
