using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Categories
{
    public interface ICategoriesActions : ICategoryFormActions
    {
        public const string NameOfCategoryRead = nameof(ICategoriesActions.CategoryRead);
        public const string NameOfCategoryDelete = nameof(ICategoriesActions.CategoryDelete);

        Task<IActionResult> CategoryRead(int? id);
        Task<IActionResult> CategoryDelete(int? id);
        
    }
}
