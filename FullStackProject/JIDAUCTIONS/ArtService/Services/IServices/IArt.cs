using ArtService.Models;
using ArtService.Models.Dtos;

namespace ArtService.Services.IServices
{
    public interface IArt
    {
        Task<List<Art>> GetAllArtworksAsync();
        Task<Art> GetArtworkByIdAsync(Guid Id);
        Task<ResponseDto> CreateArtworkAsync(AddArtDto artDto);
        Task<bool> DeleteArtworkAsync(Guid Id);
        Task <string> UpdateArtworkAsync();
    }
}
