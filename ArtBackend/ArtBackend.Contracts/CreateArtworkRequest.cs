namespace ArtBackend.Contracts.Artworks;

public record CreateArtworkRequest(
    string Title,
    string Type,
    string ImageUrl,
    int? Year,
    string Medium,
    string Size,
    decimal? Price
);