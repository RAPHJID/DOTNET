using AutoMapper;
using BidService.Models;
using BidService.Models.DTOs;
using BidService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BidService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBid _bidService;
        private readonly IArt _artservice;
        private readonly ResponseDTO _responseDto;
        private readonly IMapper _mapper;
        public BidController(IBid bidService, IMapper mapper, IArt artservice)
        {
            _bidService = bidService;
            _responseDto = new ResponseDTO();
            _mapper = mapper;
            _artservice = artservice;

        }
        [HttpPost("{Id}")]

        public async Task<ActionResult<ResponseDTO>> AddBid(BidDTO newBid, string Id)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.ErrorMessage = "Sorry You aren't Eligible";
                return Unauthorized(_responseDto);
            }

            var art = await _artservice.GetArtById(Id);
            if(string.IsNullOrWhiteSpace(art.Description))
            {
                _responseDto.ErrorMessage = "Art doesn't Exist!!";
                return NotFound(_responseDto);
            }

            var bid = _mapper.Map<Bid>(newBid);
            bid.BidderId = Guid.Parse(UserId);
            bid.ArtId = art.Id;
           

            var res = await _bidService.AddBidAsync(bid);
            _responseDto.Result = res;
            return Created($"{bid.BidId}", _responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAll()
        {
            var res = await _bidService.GetAllBidsAsync();
            _responseDto.Result = res;
            return Ok(_responseDto);
        }
    }
}
