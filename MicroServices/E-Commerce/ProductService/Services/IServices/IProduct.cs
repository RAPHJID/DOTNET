using ProductService.Models;
using ProductService.Models.Dtos;

namespace ProductService.Services.IServices
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(Guid Id);
        Task<string> AddProduct(Product product);
        Task<string> UpdateProduct();
        Task<string> DeleteProduct(Product product);

    }
}
