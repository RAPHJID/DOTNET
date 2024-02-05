using PaymentsService.Models;
using PaymentsService.Models.Dtos;

namespace PaymentsService.Services.IServices
{
    public interface IPayment
    {
        Task<string> AddPayment(Payment payment);

        Task saveChanges();

        Task<List<Payment>> GetAll(Guid bidderId);

        Task<Payment> GetPaymentById(Guid Id);

        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);


        Task<bool> ValidatePayments(Guid PaymentId);
        Task<Payment> GetOrderbyBidId(Guid bidId);
    }
}
