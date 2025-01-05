namespace TheInternshipHub.Server.Domain.Interfaces
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadAsync(byte[] fileData, string fileName, string containerName = "");
    }
}
