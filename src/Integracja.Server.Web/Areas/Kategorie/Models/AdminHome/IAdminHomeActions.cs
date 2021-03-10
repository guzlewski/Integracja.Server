using Integracja.Server.Web.Models.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.AdminHome
{
    public interface IAdminHomeActions : ICategoryFormActions
    {
        Task<IActionResult> CategoryRead(int? id);
        Task<IActionResult> CategoryDelete(int? id);
    }
}
