using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentsService.Models.Dtos;
using PaymentsService.Services.IServices;

namespace PaymentsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IArt _artService;
        private readonly ResponseDto _responseDto;
    }

}
