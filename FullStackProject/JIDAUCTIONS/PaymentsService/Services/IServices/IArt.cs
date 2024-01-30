using PaymentsService.Models.Dtos;

namespace PaymentsService.Services.IServices
{
    public interface IArt
    {
        Task<ArtDto> GetById(Guid ArtId);
    }
}
