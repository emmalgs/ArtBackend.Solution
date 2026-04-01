namespace ArtBackend.Contracts.Artworks;

public record UpdateArtworkRequest(
    string Title,
    string Type,
    int? Year,
    string Medium,
    string Size,
    decimal? Price,
    bool Sold
);
