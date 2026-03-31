using ArtBackend.Domain.Entities;
using ArtBackend.Domain.Interfaces;

public class ArtworkService : IArtworkService
{
  private readonly IArtworkRepository _repo;
  private readonly IStorageService _storage;

  public ArtworkService(IArtworkRepository repo, IStorageService storage)
  {
    _repo = repo;
    _storage = storage;
  }

  public Task<List<Artwork>> GetAllAsync()
  {
    return _repo.GetAllAsync();
  }

  public async Task<Artwork> CreateAsync(Artwork artwork, Stream imageStream, string fileName, string contentType)
  {
    var imageUrl = await _storage.UploadAsync(imageStream, fileName, contentType);
    artwork.ImageUrl = imageUrl;
    return await _repo.CreateAsync(artwork);
  }
}