using ArtBackend.Domain.Interfaces;
using Google;
using Google.Cloud.Storage.V1;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace ArtBackend.Infrastructure.Storage;

public class GoogleCloudStorageService : IStorageService
{
    private readonly StorageClient _client;
    private readonly string _bucketName;
    private const int MaxDimension = 2000;

    public GoogleCloudStorageService(IConfiguration configuration)
    {
        _client = StorageClient.Create();
        _bucketName = configuration["GCS:BucketName"]
            ?? throw new InvalidOperationException("GCS:BucketName is not configured.");
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        using var image = await Image.LoadAsync(fileStream);

        if (image.Width > MaxDimension || image.Height > MaxDimension)
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(MaxDimension, MaxDimension),
                Mode = ResizeMode.Max
            }));

        var webpFileName = Path.ChangeExtension(fileName, ".webp");

        using var outputStream = new MemoryStream();
        await image.SaveAsync(outputStream, new WebpEncoder { Quality = 85 });
        outputStream.Position = 0;

        await _client.UploadObjectAsync(_bucketName, webpFileName, "image/webp", outputStream);
        return $"https://storage.googleapis.com/{_bucketName}/{webpFileName}";
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
