
using WebApplication1.Models;

namespace WebApplication1.Service.IService
{
    public interface ICoupon
    {
        Task<ResponseDto> GetCouponAsync(string couponCode);
        Task<ResponseDto> GetAllCouponsAsync();
        Task<ResponseDto> GetCouponById(int id);
        Task<ResponseDto> AddCouponAsync(CouponDto couponDto);
        Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto> DeleteCouponAsync(int id);

    }
}
