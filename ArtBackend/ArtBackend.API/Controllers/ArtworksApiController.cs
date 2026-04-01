using ArtBackend.Contracts.Artworks;
using ArtBackend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/artworks")]
public class ArtworksApiController : ControllerBase
{
    private readonly IArtworkService _service;

    public ArtworksApiController(IArtworkService service)
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
    public async Task<IActionResult> Create([FromForm] CreateArtworkRequest request, IFormFile image)
    {
        var artwork = new Artwork
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Type = request.Type,
            Year = request.Year,
            Medium = request.Medium,
            Size = request.Size,
            Price = request.Price,
            Sold = false
        };

        using var stream = image.OpenReadStream();
        var created = await _service.CreateAsync(artwork, stream, image.FileName, image.ContentType);
        return CreatedAtAction(nameof(GetAll), created);
    }
}
