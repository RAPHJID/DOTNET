
using AutoMapper;
using BidService.Models;
using BidService.Models.DTOs;

namespace BidService.Profiles
{
    public class BidsProfiles: Profile
    {
        public BidsProfiles()
        {
            
            CreateMap<Bid, BidDTO>();
            CreateMap<BidCreateDTO, Bid>();
        }
    }
}
