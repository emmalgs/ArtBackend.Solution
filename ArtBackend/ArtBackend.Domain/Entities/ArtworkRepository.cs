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

    public async Task<Artwork> CreateAsync(Artwork artwork)
    {
        _context.Artworks.Add(artwork);
        await _context.SaveChangesAsync();
        return artwork;
    }
}