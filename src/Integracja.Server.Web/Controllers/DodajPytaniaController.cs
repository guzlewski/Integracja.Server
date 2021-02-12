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

        public IActionResult Index()
        {

            // przykład na wypełnienie kategorii 
            /*int userId = Int32.Parse(UserManager.GetUserId(User));
            Model.Categories = (IEnumerable<CategoryGetAll>)CategoryService.GetAll(userId).Result;*/

            return View(Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[Route("DodajPytania/AddCategory")]
        public IActionResult AddCategory( string categoryName )
        {
            // to jest post który mi nie działa; patrz na Views/DodajPytania/Index.cshtmml - tam jest coś nie tak
            return Content("post content: " + categoryName);
        }
    }
}
