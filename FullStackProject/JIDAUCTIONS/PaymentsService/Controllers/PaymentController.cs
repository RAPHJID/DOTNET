using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentsService.Models;
using PaymentsService.Models.Dtos;
using PaymentsService.Services;
using PaymentsService.Services.IServices;
using Stripe.Climate;
using System.Security.Claims;

namespace PaymentsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPayment _paymentService;
        private readonly ResponseDto _responseDto;
        private readonly IBid _bidService;

        public PaymentController(IMapper mapper, IPayment payment, IBid bidService)
        {
            _bidService = bidService;
            _mapper = mapper;
            _paymentService = payment;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> MakePayment(MakePaymentsDto payDto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.ErrorMessage = "Please login to Bid";
                return Unauthorized(_responseDto);
            }
            var bid = await _bidService.GetBidById(payDto.BidId);
            if (!string.IsNullOrEmpty(bid.ErrorMessage))
            {
                _responseDto.ErrorMessage = bid.ErrorMessage;
                return BadRequest(_responseDto);
            }
            var payment = _mapper.Map<Payment>(payDto);
            payment.BidderId = Guid.Parse(UserId);

            var bidOrder = await _paymentService.GetPaymentById(payDto.BidId);
            if (bidOrder.BidId != Guid.Empty)
            {
                _responseDto.ErrorMessage = "BidId already Exists";
                return BadRequest(_responseDto);
            }



            var response = await _paymentService.AddPayment(payment);
            _responseDto.Result = response;
            return Ok(_responseDto);

        }
        /*[HttpGet]
        public async Task<ActionResult<ResponseDto>> GetPayment()
        {
            var response = await _paymentService.GetAll();
            _responseDto.Result = response;
            return Ok(_responseDto);
        }*/
        /*[HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetAPayment(Guid Id)
        {
            var order = await _paymentService.GetPaymentById(Id);
            if (order == null)
            {
                return NotFound(_responseDto);
            }
            var bid = await _bidService.GetBidById(order.BidId);
            if (bid.ArtId == Guid.Empty)
            {
                return NotFound(_responseDto);
            }

            _responseDto.Result = order;
            return Ok(_responseDto);
        }*/

    }



}
