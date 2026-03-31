namespace ArtBackend.Domain.Entities;

public class Artwork
{
  public Guid Id { get; set; }
  public string Title { get; set; } = "";
  public string Type { get; set; } = "";
  public string ImageUrl { get; set; } = "";
  public int? Year { get; set; }
  public string Medium { get; set; } = "";
  public string Size { get; set; } = "";
  public decimal? Price { get; set; }
  public bool Sold { get; set; }
}