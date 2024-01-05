using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
        /*public DbSet<ProductImage> productImages {  get; set; }  */
        
            
        
    }
}
