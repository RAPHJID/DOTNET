using CartService.Models;
using CartService.Services.IServices;
using CartService.Data;
using Microsoft.EntityFrameworkCore;

namespace CartService.Services
{
    public class CouponService : ICoupon
    {
        private readonly AppDbContext _appDbContext;

        public CouponService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<decimal> CalculateDiscount(string code, decimal ogPrice)
        {
            var coupon = await GetCoupon(code);

            if (coupon != null && IsCouponValid(coupon))
            {
                //discount
                return ogPrice * (1 - coupon.DiscountPercentage / 100);
            }

            // No valid coupon or discount
            return ogPrice;
        }

        public async Task<Coupon> GetCoupon(string couponCode)
        {
            return await _appDbContext.Coupons
            .FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        }

        public async Task<bool> IsCouponValid(string couponCode)
        {
            var coupon = await GetCoupon(couponCode);

            return coupon != null && IsCouponValid(coupon);
        }
        private bool IsCouponValid(Coupon coupon)
        {
            return coupon.ExpiryDate >= DateTime.UtcNow;
        }
    }
}
