namespace ArtBackend.Domain.Interfaces;

public interface IStorageService
{
  Task<string> UploadAsync(Stream fileSteam, string fileName, string contentType);
  Task DeleteAsync(string fileName);
}