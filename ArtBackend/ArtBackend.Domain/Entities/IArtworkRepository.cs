using ArtBackend.Domain.Entities;

public interface IArtworkRepository
{
    Task<List<Artwork>> GetAllAsync();
    Task<Artwork?> GetByIdAsync(Guid id);
    Task<Artwork> CreateAsync(Artwork artwork);
    Task<Artwork?> UpdateAsync(Artwork artwork);
    Task<bool> DeleteAsync(Guid id);
}