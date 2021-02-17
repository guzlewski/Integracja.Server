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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Controllers
{
    // kontroler jest tworzony za każdym razem gdy ladujesz/odswiezasz
    public class DodajPytaniaController : ApplicationController
    {
        private ICategoryService CategoryService { get; set; }
        private IQuestionService QuestionService { get; set; }

        private DodajPytaniaViewModel Model { get; set; }

        public DodajPytaniaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            CategoryService = new CategoryService(new CategoryRepository(dbContext), AutoMapperConfig.Initialize());
            QuestionService = new QuestionService(new QuestionRepository(dbContext), AutoMapperConfig.Initialize());
            Model = new DodajPytaniaViewModel();
        }

        [HttpGet]
        public IActionResult Index( int ?id )
        {
            if( id.HasValue )
                Model.QuestionViewModel.Question.CategoryId = id.Value;
            Model.Categories = CategoryService.GetAll(UserId).Result;
            return View(Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("DodajPytania/Index/{id?}/QuestionAdd")]
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
            return RedirectToAction("Index", "DodajPytania");
        }

        public IActionResult CategorySelect( int? categoryId )
        {
            return RedirectToAction("Index", "DodajPytania", new { id = categoryId } );
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("DodajPytania/CategoryAdd")]
        // taki post działa i można teraz zmieniać dowolnie nazwy pól w DodajPytaniaViewModel
        public async Task<IActionResult> CategoryAdd([Bind(Prefix = nameof(DodajPytaniaViewModel.NewCategory))] CategoryAdd category)
        {
            // czy jest lepsza metoda niż await async i ewentualnie Route ? 
            // próbuję tylko odświeżyć stronę po dodaniu kategorii
            // Nie ogarniam do końca jak to zrobić z View, RedirectToPage itd
            // zwracają błędy i/lub nie jest dodana kategoria
            await CategoryService.Add(category, UserId);
            return RedirectToAction("Index", "DodajPytania");
        }
    }
}
