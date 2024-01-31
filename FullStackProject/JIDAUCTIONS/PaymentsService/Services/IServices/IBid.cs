using PaymentsService.Models.Dtos;

namespace PaymentsService.Services.IServices
{
    public interface IBid
    {
        Task<BidResponseDto> GetBidById(Guid Id);
    }
}
