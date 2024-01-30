
using AutoMapper;
using BidService.Models;
using BidService.Models.DTOs;

namespace BidService.Profiles
{
    public class BidsProfile: Profile
    {
        public BidsProfile()
        {
            
            CreateMap<Bid, BidDTO>().ReverseMap();
            CreateMap<BidCreateDTO, Bid>().ReverseMap();
        }
    }
}
