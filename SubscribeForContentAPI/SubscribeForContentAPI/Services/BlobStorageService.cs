using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.StaticFiles;
using SubscribeForContentAPI.Services.Contracts;

namespace SubscribeForContentAPI.Services
{
    public class BlobStorageService : IBlobStorage
    {
        private readonly int _expirationTimeInMin;
        private readonly BlobServiceClient _blobService;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobService = blobServiceClient;
            _expirationTimeInMin = 720;
        }

        public async Task<string> UploadFileAsync(string containerName, string fileName, Stream data)
        {
            containerName = containerName.Replace(" ", "-").ToLower();

            var containerClient =_blobService.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None).ConfigureAwait(false);

            var blobClient = containerClient.GetBlobClient(fileName);
            var contentType = GetContentType(fileName);
            var blobHttpHeader = new BlobHttpHeaders { ContentType = contentType };

            await blobClient.UploadAsync(data, new BlobUploadOptions { HttpHeaders = blobHttpHeader }).ConfigureAwait(false);

            return blobClient.Name;
        }

        public async Task<string> UploadFileAsync(string containerName, Stream data, string fileFormat)
        { 
            var fileName = Guid.NewGuid().ToString("N");
            fileName = $"{fileName}{fileFormat}";

            await UploadFileAsync(containerName, fileName, data);

            return fileName;
        }

        public async Task<string> GetSasUrlAsync(string containerName, string fileName)
        {
            containerName = containerName.Replace(" ", "-").ToLower();

            var containerClient = _blobService.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var exists = await blobClient.ExistsAsync();

            if (exists != null && exists.Value)
            {
                var permissions = BlobSasPermissions.Read;
                var url = blobClient.GenerateSasUri(permissions, DateTimeOffset.UtcNow.AddMinutes(_expirationTimeInMin)).ToString();

                return url;
            }
            else
                return String.Empty;
        }

        private string GetContentType(string fileName)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }
    }
}
