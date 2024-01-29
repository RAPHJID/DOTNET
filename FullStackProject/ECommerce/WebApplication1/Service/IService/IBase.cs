
using WebApplication1.Models;

namespace WebApplication1.Service.IService

{
    public interface IBase
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
