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

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
    }

}
