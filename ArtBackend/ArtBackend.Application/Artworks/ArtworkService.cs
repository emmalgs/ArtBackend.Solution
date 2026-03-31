using ArtBackend.Domain.Entities;

public class ArtworkService : IArtworkService
{
    private readonly IArtworkRepository _repo;

    public ArtworkService(IArtworkRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Artwork>> GetAllAsync()
    {
        return _repo.GetAllAsync();
    }

    public Task<Artwork> CreateAsync(Artwork artwork)
    {
        return _repo.CreateAsync(artwork);
    }
}