using Integracja.Server.Web.Areas.Historia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Historia.Controllers
{
    [Area("Historia")]
    public class HistoryUserController : Controller
    {
        public IActionResult Index(int userId)
        {
            HistoryUserViewModel Model = new HistoryUserViewModel();
            Model.userId = userId;

            return View("HistoryUser", Model);
        }


    }
}
