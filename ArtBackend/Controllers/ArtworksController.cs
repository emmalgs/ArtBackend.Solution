using ArtBackend.Contracts.Artworks;
using ArtBackend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("artworks")]
public class ArtworksController : ControllerBase
{
    private readonly IArtworkService _service;

    public ArtworksController(IArtworkService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var artworks = await _service.GetAllAsync();
        return Ok(artworks);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateArtworkRequest request)
    {
        var artwork = new Artwork
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Type = request.Type,
            ImageUrl = request.ImageUrl,
            Year = request.Year,
            Medium = request.Medium,
            Size = request.Size,
            Price = request.Price,
            Sold = false
        };

        var created = await _service.CreateAsync(artwork);
        return CreatedAtAction(nameof(GetAll), created);
    }
}
