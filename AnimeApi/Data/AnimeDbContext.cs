using AnimeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeApi.Data
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options)
            : base(options)
        {
        }
        public DbSet<Anime> Animes { get; set; }
    }
}

