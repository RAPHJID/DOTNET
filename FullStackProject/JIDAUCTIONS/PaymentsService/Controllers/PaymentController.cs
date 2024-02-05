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
        public async Task<ActionResult<ResponseDto>> AddOrder(MakePaymentsDto dto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.ErrorMessage = "Please login to Bid";
                return Unauthorized(_responseDto);
            }
            var bid = await _bidService.GetBidById(dto.BidId);
            if (!string.IsNullOrEmpty(bid.ErrorMessage))
            {
                _responseDto.ErrorMessage = bid.ErrorMessage;
                return BadRequest(_responseDto);
            }
            var order = _mapper.Map<Payment>(dto);
            order.BidderId = Guid.Parse(UserId);

            var bidOrder = await _paymentService.GetOrderbyBidId(dto.BidId);
          

            var response = await _paymentService.AddPayment(order);
            _responseDto.Result = response;
            return Ok(_responseDto);

        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> MakePayment(MakePaymentsDto payDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                _responseDto.ErrorMessage = "Please login to Bid";
                return Unauthorized(_responseDto);
            }

            var bid = await _bidService.GetBidById(payDto.BidId);

            if (bid == null)
            {
                _responseDto.ErrorMessage = "Bid not found";
                return NotFound(_responseDto);
            }

            if (!string.IsNullOrEmpty(bid.ErrorMessage))
            {
                _responseDto.ErrorMessage = bid.ErrorMessage;
                return BadRequest(_responseDto);
            }

            var payment = _mapper.Map<Payment>(payDto);
            payment.BidderId = Guid.Parse(userId);

            var bidOrder = await _paymentService.GetPaymentById(payDto.BidId);

            if (bidOrder != null)
            {
                _responseDto.ErrorMessage = "BidId already exists";
                return BadRequest(_responseDto);
            }

            var response = await _paymentService.AddPayment(payment);
            _responseDto.Result = response;

            return Ok(_responseDto);
        }



        

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetPayment()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                _responseDto.ErrorMessage = "Please login to view payments";
                return Unauthorized(_responseDto);
            }

            var response = await _paymentService.GetAll(Guid.Parse(userId)); 
            _responseDto.Result = response;
            return Ok(_responseDto);
        }


       
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetAPayment(Guid Id)
        {
            try
            {
            
                var order = await _paymentService.GetPaymentById(Id);
                if (order == null)
                {
                    _responseDto.ErrorMessage = "Payment not found";
                    return NotFound(_responseDto);
                }

                var bid = await _bidService.GetBidById(order.BidId);
                if (bid.ArtId == Guid.Empty)
                {
                    _responseDto.ErrorMessage = "Associated bid not found";
                    return NotFound(_responseDto);
                }
                _responseDto.Result = order;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
               
                _responseDto.ErrorMessage = "An error occurred while processing the request";
                return StatusCode(500, _responseDto);
            }
        }

        [HttpPost("pay")]
        public async  Task<ActionResult<ResponseDto>> MakePayment(StripeRequestDto dto)
        {
            var sR = await _paymentService.MakePayments(dto);

            _responseDto.Result = sR;
            return Ok(_responseDto);
        }

       

        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {
           
            var res = await _paymentService.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.ErrorMessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }

    }



}
