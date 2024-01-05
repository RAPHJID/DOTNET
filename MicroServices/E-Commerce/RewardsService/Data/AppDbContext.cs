using Microsoft.EntityFrameworkCore;
using RewardsService.Models;

namespace RewardsService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reward> Rewards { get; set; }

    }
}
