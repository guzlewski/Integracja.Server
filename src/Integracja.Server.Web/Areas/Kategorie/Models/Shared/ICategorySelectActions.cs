using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Kategorie.Models.Shared
{
    public interface ICategorySelectActions
    {
        Task<IActionResult> CategoryRead(int? id);
    }
}
