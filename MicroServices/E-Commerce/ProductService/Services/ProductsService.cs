using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;
using ProductService.Models.Dtos;
using ProductService.Services.IServices;

namespace ProductService.Services
{
    public class ProductsService : IProduct
    {
        private AppDbContext _dbContext;
        public ProductsService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<string> AddProduct(Product product)
        {
            _dbContext.products.Add(product);
                await _dbContext.SaveChangesAsync();
            return "Product Added Successfully!!";
        }

        public async Task<string> DeleteProduct(Product product)
        {
            _dbContext.products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return "Product Removed Successfully!!";
        }
        public async Task<Product> GetProduct(Guid Id)
        {
            return await _dbContext.products.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.products.ToListAsync();
        }

        public async Task<string> UpdateProduct()
        {
            await _dbContext.SaveChangesAsync();
            return "Product Updated Successfully!!";
        }
    }
}
