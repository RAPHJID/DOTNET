using PaymentsService.Models.Dtos;

namespace PaymentsService.Services.IServices
{
    public interface IBidder
    {
        Task<BidderDto> GetUserById(string Id);
    }
}
