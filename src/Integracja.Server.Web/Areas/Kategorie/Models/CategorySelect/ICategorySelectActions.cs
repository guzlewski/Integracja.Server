using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategorySelect
{
    public interface ICategorySelectActions
    {
        Task<IActionResult> CategoryRead(int? id);
    }
}
