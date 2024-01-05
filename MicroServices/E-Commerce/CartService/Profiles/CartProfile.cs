using AutoMapper;
using CartService.Models;
using CartService.Models.Dtos;

namespace CouponService.Profiles
{
    public class CartProfile:Profile
    {
        public CartProfile()
        {
            CreateMap<AddCart, Cart>().ReverseMap();
        }
    }
}
