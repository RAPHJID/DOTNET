using ArtAuctionblazor.Models;
using ArtAuctionblazor.Models.Arts;

namespace ArtAuctionblazor.Services.Art
{
    public interface IArt
    {
        Task<List<ArtDto>> GetArtsAsync();
        Task<ArtDto> GetArtByIdAsync(Guid id);
        Task<ResponseDto> AddArt(ArtRequestDto art);
        Task<ResponseDto> deleteArt (Guid id);
        Task<ResponseDto> updateArt(Guid id, ArtRequestDto artRequestDto);
    }
}
