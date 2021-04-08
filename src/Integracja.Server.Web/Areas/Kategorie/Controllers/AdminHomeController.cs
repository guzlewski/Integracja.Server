using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Kategorie.Models.AdminHome;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Kategorie.Controllers
{
    [Area("Kategorie")]
    public class AdminHomeController : ApplicationController, IAdminHomeActions
    {
        public AdminHomeViewModel Model { get; set; }
        public static new string Name { get => "AdminHome"; }

        public AdminHomeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int? id)
        {
            Model = new AdminHomeViewModel();
            Model.Categories = (List<CategoryDto>)await CategoryService.GetAll<CategoryDto>(UserId);
            Model.Alerts = GetAlerts();

            if (id.HasValue) // forma kategorii do wyświetlania i edycji jest wbudowana w widok
            {
                var category = await CategoryService.Get<CategoryModel>(id.Value, UserId);
                Model.CategoryFormModel.Category = category;
                Model.CategoryFormModel.ViewMode = Web.Models.Shared.Enums.ViewMode.Updating;
            }

            return View("AdminHome", Model);
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            await CategoryService.Delete(id, UserId);
            SetAlert(CategoryAlert.DeleteSuccess());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CategoryUpdate(CategoryModel category)
        {
            await CategoryService.Update(category.Id.Value, Mapper.Map<EditCategoryDto>(category), UserId);
            SetAlert(CategoryAlert.UpdateSuccess());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CategoryCreate(CategoryModel category)
        {
            int categoryId = await CategoryService.Add(Mapper.Map<CreateCategoryDto>(category), UserId);
            SetAlert(CategoryAlert.CreateSuccess());
            return RedirectToAction("Index");
        }

        public Task<IActionResult> CategoryRead(int id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", new { id = id }));
        }
    }
}
