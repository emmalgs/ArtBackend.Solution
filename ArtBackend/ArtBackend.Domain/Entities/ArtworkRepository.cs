using ArtBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ArtworkRepository : IArtworkRepository
{
    private readonly AppDbContext _context;

    public ArtworkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Artwork>> GetAllAsync()
    {
        return await _context.Artworks.ToListAsync();
    }

    public async Task<Artwork?> GetByIdAsync(Guid id)
    {
        return await _context.Artworks.FindAsync(id);
    }

    public async Task<Artwork> CreateAsync(Artwork artwork)
    {
        _context.Artworks.Add(artwork);
        await _context.SaveChangesAsync();
        return artwork;
    }

    public async Task<Artwork?> UpdateAsync(Artwork artwork)
    {
        var existing = await _context.Artworks.FindAsync(artwork.Id);
        if (existing is null) return null;

        existing.Title = artwork.Title;
        existing.Type = artwork.Type;
        existing.Year = artwork.Year;
        existing.Medium = artwork.Medium;
        existing.Size = artwork.Size;
        existing.Price = artwork.Price;
        existing.Sold = artwork.Sold;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var artwork = await _context.Artworks.FindAsync(id);
        if (artwork is null) return false;

        _context.Artworks.Remove(artwork);
        await _context.SaveChangesAsync();
        return true;
    }
}