# ArtBackend

ASP.NET Core backend for managing and serving artwork data, with PostgreSQL for persistence and Google Cloud Storage for image uploads.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Google Cloud CLI](https://cloud.google.com/sdk/docs/install)
- [dotnet-ef CLI tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install the EF Core CLI tool if you don't have it:
```bash
dotnet tool install --global dotnet-ef
```

## Running Locally

### 1. Start Postgres

```bash
docker run --name artbackend-db -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=artbackend -p 5432:5432 -d postgres
```

### 2. Configure settings

Update `appsettings.Development.json` with your GCS bucket name:
```json
"GCS": {
  "BucketName": "your-bucket-name"
}
```

### 3. Run EF Core migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Authenticate with GCS

```bash
gcloud auth application-default login
```

### 5. Run the app

```bash
dotnet run
```

The API will be available at `http://localhost:5054`.

## Endpoints

### Get all artworks
```bash
curl http://localhost:5054/artworks
```

### Create an artwork
```bash
curl -X POST http://localhost:5054/artworks \
  -F "Title=My Painting" \
  -F "Type=Oil" \
  -F "Year=2024" \
  -F "Medium=Canvas" \
  -F "Size=24x36" \
  -F "Price=500" \
  -F "image=@/path/to/image.jpg"
```
