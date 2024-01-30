using BidService.Models;

namespace BidService.Services.IServices
{
    public interface IBid
    {
        Task<string> AddBidAsync(Bid bid);
        Task<List<Bid>> GetAllBidsAsync();
        Task<Bid> GetBidByIdAsync(Guid Id);
        Task<string> DeleteBidAsync(Bid bid);
        Task<Bid> GetHighestBidForArtAsync(Guid artId);
    }
}
