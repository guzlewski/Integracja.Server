using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Konto
{
    public class KontoViewModel : PageModel
    {
        public KontoModel NewFile { get; set; }
        
        public KontoViewModel() : base()
        {
            NewFile = new KontoModel();
        }

        public static class ActionNames
        {
            public const string UploadPicture = nameof(IActions.UploadPicture);
        }

        public interface IActions
        {
            Task<IActionResult> UploadPicture([Bind(Prefix = nameof(NewFile))] IFormFile file); 
        }
    }

}
