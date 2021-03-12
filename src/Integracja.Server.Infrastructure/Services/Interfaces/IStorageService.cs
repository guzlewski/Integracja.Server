using System;
using System.IO;
using System.Threading.Tasks;

namespace Integracja.Server.Infrastructure.Services.Interfaces
{
    public interface IStorageService
    {
        Task<Uri> AddOrUpdate(Stream stream, string contentType, string fileName);
        Task Delete(string fileName);
    }
}
