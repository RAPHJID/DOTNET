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
            if (string.IsNullOrWhiteSpace(art.Description))
            {
                _responseDto.ErrorMessage = "Art doesn't Exist!!";
                return NotFound(_responseDto);
            }

            // Get the current highest bid for the art
            var currentHighestBid = await _bidService.GetHighestBidForArtAsync(art.Id);

            // Check if the new bid is higher than the starting price
            if (art.StartingPrice.HasValue && newBid.BidAmount <= art.StartingPrice.Value)
            {
                _responseDto.ErrorMessage = $"Your bid must be higher than the starting price.{currentHighestBid.BidAmount}";
                return BadRequest(_responseDto);
            }

            // Check if the currentHighestBid is not null and if the new bid is higher than the current highest bid
            if (currentHighestBid != null && newBid.BidAmount <= currentHighestBid.BidAmount)
            {
                _responseDto.ErrorMessage = "Your bid must be higher than the current highest bid.";
                return BadRequest(_responseDto);
            }

            var bid = _mapper.Map<Bid>(newBid);
            bid.BidderId = Guid.Parse(UserId);
            bid.ArtId = art.Id;

            var res = await _bidService.AddBidAsync(bid);
            _responseDto.Result = res;
            return Created($"{bid.BidId}", _responseDto);
        }



        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseDTO>> GetAll()
        {
            var res = await _bidService.GetAllBidsAsync();
            _responseDto.Result = res;
            return Ok(_responseDto);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDTO>> GetBidById(Guid Id)
        {
            var res = await _bidService.GetBidByIdAsync(Id);
            if (res == null)
            {
                _responseDto.ErrorMessage = "Bid Not Found!!";
                return NotFound(_responseDto);
            }
            _responseDto.Result = res;
            return Ok(_responseDto);
        }
    }
}
