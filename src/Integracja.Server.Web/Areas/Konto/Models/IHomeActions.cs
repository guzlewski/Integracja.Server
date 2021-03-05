using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Konto.Models
{
    public interface IHomeActions
    {
        public const string NameOfUploadPicture = nameof(IHomeActions.UploadPicture);
        Task<IActionResult> UploadPicture(IFormFile newFile);
    }
}
