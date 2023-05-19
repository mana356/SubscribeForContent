namespace SubscribeForContentAPI.Services.Contracts
{
    public interface IBlobStorage
    {
        Task<string> UploadFileAsync(string containerName, Stream data, string fileFormat);
        Task<string> UploadFileAsync(string containerName, string fileName, Stream data);
        Task<string> GetSasUrlAsync(string containerName, string fileName);
    }
}
