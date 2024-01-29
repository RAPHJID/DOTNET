using ECommerceFrontEnd.Models;
using ECommerceFrontEnd.Service.IService;

namespace ECommerceFrontEnd.Service
{
    public class BaseService : IBase
    {
      

        public BaseService()
        {
            
        }
        public Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            throw new NotImplementedException();
        }

    }
}
