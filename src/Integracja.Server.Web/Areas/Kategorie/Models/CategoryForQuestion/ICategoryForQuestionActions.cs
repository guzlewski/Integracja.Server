using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
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
