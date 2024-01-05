using CartService.Models;

namespace CartService.Services.IServices
{
    public interface ICoupon
    {
        Task<Coupon> GetCoupon(string couponCode);
        Task<bool> IsCouponValid(string couponCode);
        Task <decimal> CalculateDiscount(string code, decimal ogPrice);
    }
}
