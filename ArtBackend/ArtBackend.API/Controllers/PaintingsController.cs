using ArtBackend.Application.Artworks;
using Microsoft.AspNetCore.Mvc;

[Route("art/paintings")]
public class PaintingsController : Controller
{
    private readonly IArtworkService _service;

    public PaintingsController(IArtworkService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var all = await _service.GetAllAsync();
        var paintings = all.Where(a => a.Type == "painting").ToList();
        if (!paintings.Any()) return View("Empty");

        return RedirectToAction("Detail", new { id = paintings.First().Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var all = await _service.GetAllAsync();
        var paintings = all
            .Where(a => a.Type == "painting")
            .OrderBy(a => a.Year)
            .ThenBy(a => a.Title)
            .Select(a => new ArtworkViewModel
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
            })
            .ToList();

        var current = paintings.FirstOrDefault(a => a.Id == id);
        if (current is null) return NotFound();

        var index = paintings.IndexOf(current);

        var vm = new PaintingDetailViewModel
        {
            Current = current,
            PreviousId = index > 0 ? paintings[index - 1].Id : null,
            NextId = index < paintings.Count - 1 ? paintings[index + 1].Id : null,
            ByYear = paintings
                .GroupBy(a => a.Year ?? 0)
                .OrderByDescending(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList())
        };

        return View(vm);
    }
}
