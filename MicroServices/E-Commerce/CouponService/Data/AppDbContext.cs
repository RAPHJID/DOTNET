using CouponService.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
