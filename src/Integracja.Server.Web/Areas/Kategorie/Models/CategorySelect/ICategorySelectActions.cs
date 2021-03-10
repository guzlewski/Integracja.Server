using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategorySelect
{
    public interface ICategorySelectActions
    {
        Task<IActionResult> CategoryRead(int? id);
        Task<IActionResult> CategoryCreate(CategoryModel category);
        Task<IActionResult> GotoQuestionCreate(int categoryId);
        Task<IActionResult> Index(int? id);
    }
}
