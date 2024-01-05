using CartService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICoupon _couponService;

        public CouponController(ICoupon couponService)
        {
            _couponService = couponService;
        }

        [HttpGet("calculateDiscount")]
        public async Task<IActionResult> CalculateDiscount([FromQuery] string code, [FromQuery] decimal originalPrice)
        {
            var discountAmount = await _couponService.CalculateDiscount(code, originalPrice);
            return Ok(new { DiscountAmount = discountAmount });
        }

        [HttpGet("{couponCode}")]
        public async Task<IActionResult> GetCoupon(string couponCode)
        {
            var coupon = await _couponService.GetCoupon(couponCode);
            return Ok(coupon);
        }

        [HttpGet("validate/{couponCode}")]
        public async Task<IActionResult> ValidateCoupon(string couponCode)
        {
            var isValid = await _couponService.IsCouponValid(couponCode);
            return Ok(new { IsValid = isValid });
        }
    }
}
