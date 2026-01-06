using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastucture.Configuration;

namespace Restaurants.Infrastucture.Storage;

public class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;
    public async Task<string> UploadToBlobAsync (string fileName, Stream fileStream)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream);
        var blobUrl = blobClient.Uri.ToString();
        return blobUrl;
    }
    public string? GetBlobSasUrl (string? blobUrl)
    {
        if (blobUrl == null) return null;
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = _blobStorageSettings.LogosContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl)
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var sasToken = sasBuilder
            .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
            .ToString();
        return $"{blobUrl}?{sasToken}";
    }
    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        // EX: https://<account>.blob.core.windows.net/ logos/ imagen.jpg -> Obtiene el final
        return uri.Segments.Last();
    }
}