using BidService.Models.DTOs;

namespace BidService.Services.IServices
{
    public interface IArt
    {
        Task<ArtDto> GetArtById(string Id);
    }
}
