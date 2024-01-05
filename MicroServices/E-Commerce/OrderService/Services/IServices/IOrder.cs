using OrderService.Models;
using OrderService.Models.Dtos;

namespace OrderService.Services.IServices
{
    public interface IOrder
    {
        Task<string> AddOrder(Order order);
        Task saveChanges();
        Task<List<Order>> GetAllOrders(Guid userId);
        Task<Order> GetOrderById(Guid Id);
        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);
        Task<bool> ValidatePayments(Guid OrderId);
    }

}