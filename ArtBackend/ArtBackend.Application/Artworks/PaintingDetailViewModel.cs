namespace ArtBackend.Application.Artworks;

public class PaintingDetailViewModel
{
    public ArtworkViewModel Current { get; set; } = null!;
    public Guid? PreviousId { get; set; }
    public Guid? NextId { get; set; }
    public Dictionary<int, List<ArtworkViewModel>> ByYear { get; set; } = new();
}
