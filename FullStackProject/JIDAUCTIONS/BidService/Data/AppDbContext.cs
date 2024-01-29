

using BidService.Models;
using Microsoft.EntityFrameworkCore;

namespace BidService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Bid> Bids { get; set; }
    }
}
