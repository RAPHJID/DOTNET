using BiddingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BiddingService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Bid> Bids { get; set; }
    }
}
