using Integracja.Server.Web.Areas.Kategorie.Models.CategorySelect;
using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategoryForQuestion
{
    public interface ICategoryForQuestionActions : ICategorySelectActions, ICategoryFormActions
    {
        Task<IActionResult> GotoQuestionCreate(int categoryId);
        Task<IActionResult> Index(int? id);
    }
}
