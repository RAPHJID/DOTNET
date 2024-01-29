using ArtService.Models;
using ArtService.Models.Dtos;
using AutoMapper;

namespace ArtService.Profiles
{
    public class ArtsProfiles: Profile
    {
        public ArtsProfiles()
        {
            CreateMap<AddArtDto, Art>().ReverseMap();
        }
    }
}
