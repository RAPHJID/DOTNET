using ArtService.Data;
using ArtService.Models;
using ArtService.Models.Dtos;
using ArtService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArtService.Services
{
    public class ArtServices : IArt
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public ArtServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        /*public async Task<ResponseDto> CreateArtworkAsync(AddArtDto artDto)
        {
            
        }*/

        public async Task<ResponseDto> CreateArtworkAsync(AddArtDto artDto)
        {
            var mappedArt = _mapper.Map<Art>(artDto);
            _context.Arts.Add(mappedArt);
            await _context.SaveChangesAsync();

            _response.ErrorMessage = "Art Added Successfully!!";
            _response.Result = mappedArt;
            return _response;
        }

        public async Task<bool> DeleteArtworkAsync(Guid Id)
        {
            var artToRemove = await _context.Arts.FirstOrDefaultAsync(a => a.Id == Id);

            if (artToRemove != null)
            {
                _context.Arts.Remove(artToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Art>> GetAllArtworksAsync()
        {
            return await _context.Arts.ToListAsync();
            
        }

        public async Task<Art> GetArtworkByIdAsync(Guid Id)
        {
            return await _context.Arts.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateArtworkAsync()
        {
            await _context.SaveChangesAsync();
            return "Art Updated Successfully!!";
        }
    }
}
