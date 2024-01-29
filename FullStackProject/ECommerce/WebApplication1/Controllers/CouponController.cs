using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Service.IService;

namespace WebApplication1.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICoupon _coupon;
        public CouponController(ICoupon couponService)
        {
            _coupon = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> list = new();
            ResponseDto response = await _coupon.GetAllCouponsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
