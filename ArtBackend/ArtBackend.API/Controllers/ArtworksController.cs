using ArtBackend.Application.Artworks;
using ArtBackend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("artworks")]
public class ArtworksController : Controller
{
    private readonly IArtworkService _service;

    public ArtworksController(IArtworkService service)
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
}
