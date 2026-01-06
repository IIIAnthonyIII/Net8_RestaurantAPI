namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(string fileName, Stream fileStream);
    string? GetBlobSasUrl (string? blobUrl);
}