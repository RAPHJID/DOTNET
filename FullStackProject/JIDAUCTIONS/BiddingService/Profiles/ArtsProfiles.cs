
using AutoMapper;

namespace BiddingService.Profiles
{
    public class ArtsProfiles: Profile
    {
        public ArtsProfiles()
        {
            CreateMap<AddArtDto, Art>().ReverseMap();
        }
    }
}
