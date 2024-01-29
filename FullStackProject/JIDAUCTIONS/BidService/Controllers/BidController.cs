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
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddBid(BidDTO newBid)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.ErrorMessage = "Sorry You aren't Eligible";
                return Unauthorized(_responseDto);
            }

            var art = _mapper.Map<Bid>(newBid);
            art.BidderId = Guid.Parse(UserId);

            var res = await _bidService.AddBidAsync(art);
            _responseDto.Result = res;
            return Created($"{art.BidId}", _responseDto);
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
