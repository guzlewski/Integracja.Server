using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers
{
    // kontroler jest tworzony za każdym razem gdy ladujesz/odswiezasz
    public class DodajPytaniaController : ApplicationController
    {
        private DodajPytaniaViewModel Model { get; set; }

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
        public IActionResult Category( int ?id )
        {
            if( id.HasValue )
                Model.QuestionViewModel.Question.CategoryId = id.Value;
            Model.Categories = CategoryService.GetAll(UserId).Result;
            return View("~/Views/DodajPytania/Index.cshtml",Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("DodajPytania/Index/{id?}/QuestionAdd")]
        [ActionName("QuestionAdd")]
        public async Task<IActionResult> QuestionAdd(
            int? id,
            [Bind(Prefix = nameof(DodajPytaniaViewModel.QuestionViewModel.Question))] QuestionAdd question,
            [Bind(Prefix = nameof(DodajPytaniaViewModel.QuestionViewModel.Answers))] List<AnswerDto> answers)
        {
            // dodawanie pytania
            if( id.HasValue )
                question.CategoryId = id.Value;

            question.Answers = answers;
            await QuestionService.Add(question, UserId);
            return RedirectToAction("Index", "DodajPytania", new { id = id } );
        }

        public IActionResult CategorySelect( int? categoryId )
        {
            return RedirectToAction("Index", "DodajPytania", new { id = categoryId } );
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
