using ArtBackend.Domain.Entities;

public interface IArtworkRepository
{
    Task<List<Artwork>> GetAllAsync();
    Task<Artwork> CreateAsync(Artwork artwork);
}