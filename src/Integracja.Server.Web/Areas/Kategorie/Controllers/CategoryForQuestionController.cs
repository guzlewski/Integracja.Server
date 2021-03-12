﻿using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Kategorie.Models.CategoryForQuestion;
using Integracja.Server.Web.Areas.Pytania.Controllers;
using Integracja.Server.Web.Areas.Pytania.Models.Question;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Controllers
{
    [Area("Kategorie")]
    public class CategoryForQuestionController : ApplicationController, ICategoryForQuestionActions
    {
        private CategoryForQuestionViewModel Model { get; set; }
        public static new string Name { get => "CategoryForQuestion"; }

        public CategoryForQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }
        
        public async Task<IActionResult> Index(int? id)
        {
            Model = new CategoryForQuestionViewModel();
            Model.CategorySelectModel.Categories = CategoryModel.ConvertToList(await CategoryService.GetAll(UserId));

            if (id.HasValue)
            {
                Model.CategoryFormModel.Category.Id = id.Value;
            }

            return View("CategoryForQuestion", Model);
        }

        public async Task<IActionResult> CategoryRead(int? id)
        {
            return RedirectToAction("Index", new { id = id });
        }

        public async Task<IActionResult> CategoryCreate(CategoryModel category)
        {
            int categoryId = await CategoryService.Add(category.ToCategoryAdd(), UserId);
            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> GotoQuestionCreate(int id)
        {
            return RedirectToAction(nameof(IQuestionActions.QuestionCreateViewStep2), QuestionController.Name, new { area = "Pytania", categoryId = id });
        }

        public Task<IActionResult> CategoryUpdate(CategoryModel category)
        {
            // nie daję możliwości zaaktualizowania kategorii stąd
            throw new System.NotImplementedException();
        }
    }
}