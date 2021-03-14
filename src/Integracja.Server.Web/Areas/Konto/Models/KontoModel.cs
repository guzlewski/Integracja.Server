using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Areas.Konto.Models
{
    public class KontoModel
    {
        [TempData]
        public string ErrorMessage { get; set; }

        [Required(ErrorMessage = "Nie wybrano żadnego pliku.")]
        public IFormFile File { get; set; }


    }

}
