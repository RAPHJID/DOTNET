using CouponService.Data;
using CouponService.Models;
using CouponService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Services
{
    public class CouponsService : ICoupon
    {
        private readonly AppDbContext _appDbContext;
        public CouponsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> AddCoupon(Coupon coupon)
        {
            _appDbContext.Coupons.Add(coupon);
            await _appDbContext.SaveChangesAsync();
            return "Coupon Added Successfully!!";
        }

        public async Task<string> DeleteCoupon(Coupon coupon)
        {
            _appDbContext.Coupons.Remove(coupon);
            await _appDbContext.SaveChangesAsync();
            return "Coupon Deleted Successfully!!";
        }

        public async Task<List<Coupon>> GetAllCoupons()
        {
            return await _appDbContext.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetCoupon(Guid Id)
        {
            return await _appDbContext.Coupons.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<Coupon> GetCoupon(string code)
        {
            return await _appDbContext.Coupons.Where(x => x.CouponCode == code).FirstOrDefaultAsync();
        }
        public async Task<string> UpdateCoupon()
        {
            await _appDbContext.SaveChangesAsync();
            return "Coupon Updated Successfully!!!";
        }
    }
}
