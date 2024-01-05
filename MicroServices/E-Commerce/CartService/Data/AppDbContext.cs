using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }  
        public DbSet<Coupon> Coupons { get; set; }
    }
}
