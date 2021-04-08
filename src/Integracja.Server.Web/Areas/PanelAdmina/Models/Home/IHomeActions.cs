using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Home
{
    public interface IHomeActions
    {
        Task<IActionResult> GotoPytaniaAdminHome();
        Task<IActionResult> GotoKategorieAdminHome();
    }
}
