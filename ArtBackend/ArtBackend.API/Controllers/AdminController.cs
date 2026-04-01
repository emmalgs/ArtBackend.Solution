using ArtBackend.Application.Artworks;
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
    public async Task<IActionResult> Index()
    {
        var artworks = await _service.GetAllAsync();
        var viewModels = artworks.Select(a => new ArtworkViewModel
        {
            Id = a.Id,
            Title = a.Title,
            Type = a.Type,
            ImageUrl = a.ImageUrl,
            Year = a.Year,
            Medium = a.Medium,
            Size = a.Size,
            Price = a.Price,
            Sold = a.Sold
        }).ToList();

        return View(viewModels);
    }

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

    [HttpPost("artworks/{id}/delete")]
    public async Task<IActionResult> DeleteArtwork(Guid id, string imageFileName)
    {
        await _service.DeleteAsync(id, imageFileName);
        return RedirectToAction("Index");
    }

    [HttpPost("artworks/{id}/update")]
    public async Task<IActionResult> UpdateArtwork(Guid id, [FromForm] UpdateArtworkRequest request)
    {
        var artwork = new Artwork
        {
            Id = id,
            Title = request.Title,
            Type = request.Type,
            Year = request.Year,
            Medium = request.Medium,
            Size = request.Size,
            Price = request.Price,
            Sold = request.Sold
        };

        await _service.UpdateAsync(artwork);
        return RedirectToAction("Index");
    }
}
