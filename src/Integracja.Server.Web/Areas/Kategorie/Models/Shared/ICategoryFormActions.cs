using System.Threading.Tasks;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Kategorie.Models.Shared
{
    public interface ICategoryFormActions
    {
        Task<IActionResult> CategoryCreate(CategoryModel category);
        Task<IActionResult> CategoryUpdate(CategoryModel category);
    }
}
