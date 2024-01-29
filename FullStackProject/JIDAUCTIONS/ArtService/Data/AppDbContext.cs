using ArtService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        {
            
        }
        public DbSet<Art> Arts { get; set; }
    }
}
