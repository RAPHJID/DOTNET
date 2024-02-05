using ArtService.Models;
using ArtService.Models.Dtos;
using ArtService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private readonly IArt _artService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public ArtController(IArt art, IMapper mapper)
        {
            _artService = art;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateArtworkAsync(AddArtDto addArtDto)
        {
            //var art = _mapper.Map<Art>(addArtDto);
            var res = await _artService.CreateArtworkAsync(addArtDto);

            return Created("", res);
        }

        /* [HttpPost]
         public async Task<ActionResult<ResponseDto>> CreateArtworkAsync(AddArtDto addArtDto)
         {
             // Access the user's identity to get the user ID
             var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

             // Check if the user ID is present
             if (userId != null)
             {
                 // Now you can use the userId as needed
                 var res = await _artService.CreateArtworkAsync(addArtDto, userId);

                 return Created("", res);
             }
             else
             {
                 // Handle the case when the user ID is not found
                 // For example, return an unauthorized response or take appropriate action
                 return Unauthorized();
             }
         }*/


        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllArt()
        {
            var res = await _artService.GetAllArtworksAsync();
            _responseDto.Result = res;
            return Ok(_responseDto);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetArt(Guid Id)
        {
            var res = await _artService.GetArtworkByIdAsync(Id);
            if(res == null)
            {
                _responseDto.ErrorMessage = "Art Not Found!!";
                return NotFound(_responseDto);
            }
            _responseDto.Result = res;
            return Ok(_responseDto);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseDto>> DeleteArt(Guid Id)
        {
            var art = await _artService.GetArtworkByIdAsync(Id);
            if (art == null)
            {
                _responseDto.Result = "Art Not Found!!";
                _responseDto.IsSuccess = false;
                return NotFound(_responseDto);
            }
            var res = await _artService.DeleteArtworkAsync(Id);
            if (res) { 
            _responseDto.Result = res;
            return Ok(_responseDto);
            
            }
            _responseDto.ErrorMessage = "Art does not exist";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseDto>> UpdateArt(Guid Id, AddArtDto UArt) 
        {
            var art = await _artService.GetArtworkByIdAsync(Id);
            if(art == null)
            {
                _responseDto.Result = "Art Not Found";
                _responseDto.IsSuccess = false;
                return NotFound(_responseDto);
            }
            _mapper.Map(UArt, art);
            var res = await _artService.UpdateArtworkAsync(); 
            _responseDto.Result = res;
            return Ok(_responseDto);
        }

    }
}
