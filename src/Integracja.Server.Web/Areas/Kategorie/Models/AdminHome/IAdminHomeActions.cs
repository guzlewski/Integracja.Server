using System.Threading.Tasks;
using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Kategorie.Models.AdminHome
{
    public interface IAdminHomeActions : ICategoryFormActions
    {
        Task<IActionResult> CategoryRead(int id);
        Task<IActionResult> CategoryDelete(int id);
    }
}
