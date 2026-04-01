using ArtBackend.Domain.Entities;

public interface IArtworkService
{
    Task<List<Artwork>> GetAllAsync();
    Task<Artwork?> GetByIdAsync(Guid id);
    Task<Artwork> CreateAsync(Artwork artwork, Stream imageStream, string fileName, string contentType);
    Task<Artwork?> UpdateAsync(Artwork artwork);
    Task<bool> DeleteAsync(Guid id, string imageFileName);
}