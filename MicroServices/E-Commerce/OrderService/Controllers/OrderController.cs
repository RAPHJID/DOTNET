using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoupon _couponService;
        private readonly IOrder _orderService;
        private readonly IProduct _productService;
        private readonly ResponseDto _responseDto;

        public OrderController(IMapper mapper, IProduct product, IOrder order, ICoupon coupon)
        {
            _couponService = coupon;
            _mapper = mapper;
            _productService = product;
            _responseDto = new ResponseDto();
            _orderService = order;
        }

        [HttpPost]

        public async Task<ActionResult<ResponseDto>> AddOrder(AddOrderDto dto)
        {
            
            var order = _mapper.Map<Order>(dto);

            var product = await _productService.GetById(order.ProductId);

            if (product == null)
            {
                _responseDto.ErrorMessage = "Invalid Values";
                return NotFound(_responseDto);
            }


            var res = await _orderService.AddOrder(order);
            _responseDto.Result = res;
            return Ok(_responseDto);


        }

        [HttpPost("Pay")]

        public async Task<ActionResult<ResponseDto>> makePayments(StripeRequestDto dto)
        {

            var sR = await _orderService.MakePayments(dto);

            _responseDto.Result = sR;
            return Ok(_responseDto);
        }

        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {

            var res = await _orderService.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.ErrorMessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }

        [HttpGet("{userId}")]

        public async Task<ActionResult<ResponseDto>> GetUserBooking(Guid userId)
        {
            
            var res = await _orderService.GetAllOrders(userId);
            _responseDto.Result = res;
            return Ok(_responseDto);


        }

        [HttpPut("{Id}")]

        public async Task<ActionResult<ResponseDto>> ApplyCoupon(Guid Id, string Code)
        {
       
            var order = await _orderService.GetOrderById(Id);

            if (order == null)
            {
                _responseDto.ErrorMessage = "Order Not Found";
                return NotFound(_responseDto);
            }
            var coupon = await _couponService.GetCouponByCouponCode(Code);
            if (coupon == null)
            {
                _responseDto.ErrorMessage = "Coupon is not Valid";
                return NotFound(_responseDto);
            }

            if (coupon.CouponMinAmount <= order.TotalAmount)
            {
                order.CouponCode = coupon.CouponCode;
                order.Discount = coupon.CouponAmount;
                await _orderService.saveChanges();
                _responseDto.Result = "Code applied";
                return Ok(_responseDto);
            }
            else
            {
                
                return BadRequest(_responseDto);
            }


            return Ok(_responseDto);

        }

    }
}
