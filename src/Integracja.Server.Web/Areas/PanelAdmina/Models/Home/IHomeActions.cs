using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Home
{
    public interface IHomeActions
    {
        Task<IActionResult> GotoPytaniaAdminHome();
        Task<IActionResult> GotoKategorieAdminHome();
    }
}
