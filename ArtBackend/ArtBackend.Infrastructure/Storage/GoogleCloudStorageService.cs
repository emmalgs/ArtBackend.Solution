using ArtBackend.Domain.Interfaces;
using Google;
using Google.Cloud.Storage.V1;

namespace ArtBackend.Infrastructure.Storage;

public class GoogleCloudStorageService : IStorageService
{
    private readonly StorageClient _client;
    private readonly string _bucketName;

    public GoogleCloudStorageService(IConfiguration configuration)
    {
        _client = StorageClient.Create();
        _bucketName = configuration["GCS:BucketName"]
            ?? throw new InvalidOperationException("GCS:BucketName is not configured.");
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        await _client.UploadObjectAsync(_bucketName, fileName, contentType, fileStream);
        return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
    }

    public async Task DeleteAsync(string fileName)
    {
        try
        {
            await _client.DeleteObjectAsync(_bucketName, fileName);
        }
        catch (GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Object doesn't exist in GCS — nothing to delete
        }
    }
}
