using AuthMicroService.Model;
using AuthMicroService.Model.Dtos;
using AutoMapper;

namespace AuthMicroService.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterUserDto, AppUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email));
            CreateMap<UserDto, AppUser>().ReverseMap();
        }
    }
}
