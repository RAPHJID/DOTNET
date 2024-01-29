using AuthService.Model;
using AuthService.Model.Dtos;
using AutoMapper;

namespace AuthService.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterRequestDto, AppUser>()
            .ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));

            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
