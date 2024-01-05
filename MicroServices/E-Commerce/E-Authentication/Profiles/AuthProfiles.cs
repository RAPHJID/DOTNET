using AutoMapper;
using E_Authentication.Models;
using E_Authentication.Models.Dtos;

namespace E_Authentication.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles() 
        {
            CreateMap<RegisterUserDto, AppUser>()
                .ForMember(dest=>dest.UserName, src=>src.MapFrom(r=>r.Email));
            CreateMap<UserDto, AppUser>().ReverseMap();
        }
    }
}
