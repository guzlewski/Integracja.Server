using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Integracja.Server.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class BlobStorageService : IStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService(IOptions<BlobStorageSettings> options, BlobServiceClient blobServiceClient)
        {
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(options.Value.BlobContainerName);
        }

        public async Task<Uri> AddOrUpdate(Stream stream, string contentType, string fileName)
        {
            var client = _blobContainerClient.GetBlobClient(fileName);
            var blobHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            };

            await client.UploadAsync(stream, blobHeaders);

            return client.Uri;
        }

        public async Task Delete(string fileName)
        {
            var client = _blobContainerClient.GetBlobClient(fileName);
            await client.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }
    }
}
