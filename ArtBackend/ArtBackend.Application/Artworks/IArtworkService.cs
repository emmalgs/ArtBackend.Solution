using ArtBackend.Domain.Entities;

public interface IArtworkService
{
    Task<List<Artwork>> GetAllAsync();
    Task<Artwork> CreateAsync(Artwork artwork, Stream imageStream, string fileName, string contentType);
}