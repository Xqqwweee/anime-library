using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using AnimeApi.Data;
using AnimeApi.Models;
using AnimeApi.DTOs;
using AnimeApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnimeDbContext>(options =>
{
    options.UseSqlite("Data Source=AnimeDb.db");
});

builder.Services.AddScoped<CreateAnimeDtoValidator>();

builder.Services.AddScoped<PatchAnimeDtoValidator>();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("Angular");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/anime", async (AnimeDbContext db) =>
{
    List<AnimeResponseDto> dtos = await db.Animes
        .Select(x => AnimeResponseDto.ToDto(x))
        .ToListAsync();

    return Results.Ok(dtos);
});
app.MapGet("/anime/{id}", async (int id, AnimeDbContext db) =>
{
    var anime = await db.Animes.FindAsync(id);
    if (anime is null) return Results.NotFound();
    var dto = AnimeResponseDto.ToDto(anime);

    return Results.Ok(dto);
});
app.MapPost("/anime", async (CreateAnimeDtoValidator validator, CreateAnimeDto newAnimeDto, AnimeDbContext db) =>
{
    
    var result = await validator.ValidateAsync(newAnimeDto);

    if (!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }
    Anime newAnime = new();
    newAnime.Title = newAnimeDto.Title;
    newAnime.Episodes = newAnimeDto.Episodes;
    newAnime.Rating = newAnimeDto.Rating;


    db.Animes.Add(newAnime);
    await db.SaveChangesAsync();

    return Results.Created(
    $"/anime/{newAnime.Id}",
    AnimeResponseDto.ToDto(newAnime)
);
});
app.MapDelete("/anime/{id}", async (int id, AnimeDbContext db) => 
{
    var anime = await db.Animes.FindAsync(id);
    if (anime is null) return Results.NotFound();
    db.Animes.Remove(anime);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPut("/anime/{id}", async (int id, CreateAnimeDtoValidator validator, CreateAnimeDto updatedAnimeDto, AnimeDbContext db) =>
{
    var result = await validator.ValidateAsync(updatedAnimeDto);

    if (!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }
    Console.WriteLine($"Looking for anime id: {id}");
    var oldAnime = await db.Animes.FindAsync(id);
    Console.WriteLine(oldAnime is null ? "NOT FOUND" : $"FOUND: {oldAnime.Title}");

    if (oldAnime is null) return Results.NotFound();

    oldAnime.Title = updatedAnimeDto.Title;
    oldAnime.Episodes = updatedAnimeDto.Episodes;
    oldAnime.Rating = updatedAnimeDto.Rating;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPatch("/anime/{id}", async (int id, PatchAnimeDtoValidator validator, PatchAnimeDto dto, AnimeDbContext db) => 
{
    var result = await validator.ValidateAsync(dto);

    if(!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }

    var oldAnime = await db.Animes.FindAsync(id);

    if (oldAnime is null) return Results.NotFound(); 

    if(dto.Title is not null)
    {
        oldAnime.Title = dto.Title;
    }
    if(dto.Episodes is not null)
    {
        oldAnime.Episodes = dto.Episodes.Value;
    }
    if(dto.Rating is not null)
    {
        oldAnime.Rating = dto.Rating.Value;
    }

    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.Run();