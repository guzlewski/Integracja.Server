using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public interface IGameSettingsValidation
    {
        IActionResult VerifyDates(string startDate, string startTime, string endDate, string endTime);
    }
}
