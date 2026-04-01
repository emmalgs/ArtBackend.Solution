using ArtBackend.Contracts.Artworks;
using ArtBackend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = "AdminCookie")]
[Route("admin")]
public class AdminController : Controller
{
    private readonly IArtworkService _service;

    public AdminController(IArtworkService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost("artworks")]
    public async Task<IActionResult> CreateArtwork([FromForm] CreateArtworkRequest request, IFormFile image)
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
        await _service.CreateAsync(artwork, stream, image.FileName, image.ContentType);
        return RedirectToAction("Index");
    }
}
