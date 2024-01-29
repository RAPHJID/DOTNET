using ECommerceFrontEnd.Models;

namespace ECommerceFrontEnd.Service.IService
{
    public interface IBase
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
