using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Services;
using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Infrastructure.Repositories;
using Integracja.Server.Infrastructure.Mappers;
using Integracja.Server.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Integracja.Server.Infrastructure.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Integracja.Server.Core.Models.Identity;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace Integracja.Server.Web.Controllers
{
    // kontroler jest tworzony za każdym razem gdy ladujesz/odswiezasz
    public class DodajPytaniaController : ApplicationController
    {
        // service 
        private ICategoryService _categoryService;
        private ICategoryService CategoryService { get => _categoryService; }
        // model 
        private DodajPytaniaViewModel _model;
        private DodajPytaniaViewModel Model { get => _model; }

        public DodajPytaniaController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
            _categoryService = new CategoryService(new CategoryRepository(dbContext), AutoMapperConfig.Initialize());
            _model = new DodajPytaniaViewModel();
        }

        [HttpGet]
        public IActionResult Index()
        {
            // przykład na wypełnienie kategorii 
            Model.Categories = (IEnumerable<CategoryGetAll>)CategoryService.GetAll(UserId).Result;
            return View(Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("DodajPytania/AddCategory")]
        // taki post działa i można teraz zmieniać dowolnie nazwy pól w DodajPytaniaViewModel
        public async Task<IActionResult> AddCategory([Bind(Prefix = nameof(DodajPytaniaViewModel.NewCategory))] CategoryAdd category)
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
