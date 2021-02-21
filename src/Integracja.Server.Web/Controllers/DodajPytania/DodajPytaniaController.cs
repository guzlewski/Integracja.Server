using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers
{
    public class DodajPytaniaController : ApplicationController
    {
        private DodajPytaniaViewModel Model { get; set; }

        private string FormDataKey = "TemporaryFormData";

        public DodajPytaniaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            Model = new DodajPytaniaViewModel();
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            return RedirectToAction("Category", new { id = id });
        }

        [HttpGet]
        [ActionName("Category")]
        public IActionResult Category(int? id)
        {
            if (id.HasValue)
                Model.QuestionViewModel.QuestionForm.CategoryId = id.Value;
            Model.Categories = CategoryService.GetAll(UserId).Result;
            return View("~/Views/DodajPytania/Index.cshtml",Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[Route("DodajPytania/Index/{id?}/AnswerFieldAdd")]
        [ActionName("AnswerFieldAdd")]
        public IActionResult AnswerFieldAdd(
            int? id,
            [Bind(Prefix = nameof(DodajPytaniaViewModel.QuestionViewModel.QuestionForm))] QuestionFormModel question )
        {

            return RedirectToAction("Index", "DodajPytania", new { id = id });
        }

        public IActionResult CategorySelect(int? categoryId)
        {
            return RedirectToAction("Index", "DodajPytania", new { id = categoryId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("DodajPytania/Index/{id?}/QuestionAdd")]
        [ActionName("QuestionAdd")]
        public async Task<IActionResult> QuestionAdd(
            int? id,
            [Bind(Prefix = nameof(DodajPytaniaViewModel.QuestionViewModel.QuestionForm))] QuestionFormModel question )
        {
            // Assemble to QuestionAdd and Add
            string test = question.QuestionContent;
            return RedirectToAction("Index", "DodajPytania", new { id = id } );
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("DodajPytania/CategoryAdd")]
        [ActionName("CategoryAdd")]
        public async Task<IActionResult> CategoryAdd(
            [Bind(Prefix = nameof(DodajPytaniaViewModel.NewCategory))] CategoryAdd category)
        {
            int categoryId = await CategoryService.Add(category, UserId);
            return RedirectToAction("Index", "DodajPytania", new { id = categoryId });
        }
    }
}
