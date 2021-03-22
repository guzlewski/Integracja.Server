using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IPictureService
    {
        Task<string> Save(IFormFile formFile, int userId);
        Task Delete(int userId);
    }
}
