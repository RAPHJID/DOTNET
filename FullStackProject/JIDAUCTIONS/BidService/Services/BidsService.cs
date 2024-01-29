using AutoMapper;
using BidService.Data;
using BidService.Models;
using BidService.Models.DTOs;
using BidService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BidService.Services
{
    public class BidsService : IBid
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDTO;

        public BidsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<string> AddBidAsync(Bid bid)
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
            return "Bid Added successfully";
        }

        public async Task<string> DeleteBidAsync(Bid bid)
        {
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();
            return "Bid Deleted Successfully";
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            var bid = await _context.Bids.ToListAsync();
            return bid;
        }

        public async Task<Bid> GetBidByIdAsync(Guid Id)
        {
            return await _context.Bids.Where(x => x.BidId == Id).FirstOrDefaultAsync();
        }
    }
}
