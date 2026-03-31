using Microsoft.EntityFrameworkCore;
using ArtBackend.Domain.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Artwork> Artworks => Set<Artwork>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}