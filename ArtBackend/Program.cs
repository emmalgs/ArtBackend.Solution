using ArtBackend.Domain.Interfaces;
using ArtBackend.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);

// DI
builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
builder.Services.AddScoped<IArtworkService, ArtworkService>();
builder.Services.AddSingleton<IStorageService, GoogleCloudStorageService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();