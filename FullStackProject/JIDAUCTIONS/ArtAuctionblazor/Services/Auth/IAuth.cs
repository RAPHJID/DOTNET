using ArtAuctionblazor.Models;
using ArtAuctionblazor.Models.Auth;

namespace ArtAuctionblazor.Services.Auth
{
    public interface IAuth
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
