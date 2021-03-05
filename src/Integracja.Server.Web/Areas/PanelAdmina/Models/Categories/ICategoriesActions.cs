using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Categories
{
    public interface ICategoriesActions
    {
        public const string NameOfCategoryCreate = nameof(ICategoriesActions.CategoryCreate);
        public const string NameOfCategoryRead = nameof(ICategoriesActions.CategoryRead);
        public const string NameOfCategoryUpdate = nameof(ICategoriesActions.CategoryUpdate);
        public const string NameOfCategoryDelete = nameof(ICategoriesActions.CategoryDelete);

        Task<IActionResult> CategoryCreate(CategoryModel category);
        Task<IActionResult> CategoryRead(int? id);
        Task<IActionResult> CategoryDelete(int? id);
        Task<IActionResult> CategoryUpdate(CategoryModel category);
    }
}
