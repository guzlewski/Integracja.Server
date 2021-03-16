using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.Shared
{
    public interface ICategorySelectActions
    {
        Task<IActionResult> CategoryRead(int? id);
    }
}
