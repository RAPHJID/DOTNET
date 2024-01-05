    using CouponService.Models;

    namespace CouponService.Services.IServices
    {
        public interface ICoupon
        {
            Task<List<Coupon>> GetAllCoupons();
            Task<Coupon> GetCoupon(Guid Id);
            Task<string> AddCoupon(Coupon coupon);
            Task<string> UpdateCoupon();
            Task<string> DeleteCoupon(Coupon coupon);
            Task<Coupon> GetCoupon(string code);
        }
    }
