using OrderService.Models.Dtos;

namespace OrderService.Services.IServices
{
    public interface IProduct
    {
        Task<ProductDto> GetById(Guid id);
    }
}
