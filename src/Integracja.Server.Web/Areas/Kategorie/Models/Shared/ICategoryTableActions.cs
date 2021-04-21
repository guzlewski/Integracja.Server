using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.Shared
{
    public interface ICategoryTableActions
    {
        Task<IActionResult> GotoCategoryRead(int id);
        Task<IActionResult> GotoCategoryUpdate(int id);
        Task<IActionResult> GotoCategoryDelete(int id);
    }
}
