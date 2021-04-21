using System.Threading.Tasks;
using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategoryForQuestion
{
    public interface ICategoryForQuestionActions : ICategorySelectTableActions, ICategoryFormActions
    {
        Task<IActionResult> GotoQuestionCreate(int? id);
        Task<IActionResult> Index(int? id);
    }
}
