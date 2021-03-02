using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Konto
{
    public class KontoModel
    {
        [TempData]
        public string ErrorMessage { get; set; }

        [Required(ErrorMessage = "Nie wybrano żadnego pliku.")]
        public IFormFile File { get; set; }


    }

}
