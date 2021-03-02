using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Categories
{
    public class CategoriesViewModel : PageModel
    {
        public List<CategoryGetAll> Categories { get; set; }

        public CategoryModel Category { get; set; }

        public CategoriesViewModel() : base()
        {
            Category = new CategoryModel();
        }

        public static class ActionNames
        {
            public const string CategoryCreate = nameof(IActions.CategoryCreate);
            public const string CategoryRead = nameof(IActions.CategoryRead);
            public const string CategoryUpdate = nameof(IActions.CategoryUpdate);
            public const string CategoryDelete = nameof(IActions.CategoryDelete);
        }

        public interface IActions
        {
            Task<IActionResult> CategoryCreate(CategoryModel category);
            Task<IActionResult> CategoryRead(int? id);
            Task<IActionResult> CategoryDelete( int? id );
            Task<IActionResult> CategoryUpdate(CategoryModel category);
        }
    }
}
