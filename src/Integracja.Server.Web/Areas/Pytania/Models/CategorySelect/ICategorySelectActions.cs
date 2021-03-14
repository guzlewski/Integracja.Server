using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.CategorySelect
{
    public interface ICategorySelectActions
    {
        public const string NameOfCategoryRead = nameof(CategoryRead);
        public const string NameOfCategoryCreate = nameof(CategoryCreate);
        public const string NameOfGotoQuestionCreate = nameof(GotoQuestionCreate);

        Task<IActionResult> CategoryRead(int? id);
        Task<IActionResult> CategoryCreate(CategoryModel category);
        Task<IActionResult> GotoQuestionCreate(int categoryId);
        Task<IActionResult> Index(int? id);
    }
}
