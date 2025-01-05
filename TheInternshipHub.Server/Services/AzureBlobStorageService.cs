using Azure.Storage.Blobs;
using TheInternshipHub.Server.Domain.Interfaces;

namespace TheInternshipHub.Server.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "resumes";

        public AzureBlobStorageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
        }

        public async Task<string> UploadAsync(byte[] fileData, string filename, string containerName = "")
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(string.IsNullOrEmpty(containerName) ? _containerName : containerName);

            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(filename);

            using (var stream = new MemoryStream(fileData))
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }
    }
}
