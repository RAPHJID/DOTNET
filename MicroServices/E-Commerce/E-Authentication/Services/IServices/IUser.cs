using E_Authentication.Models;
using E_Authentication.Models.Dtos;

namespace E_Authentication.Services.IServices
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto userDto);
        Task<LoginResponseDto> loginUser(LoginRequestDto loginRequestDto);
        Task<bool> AssignUserRoles(string Email, string RoleName);
        Task<AppUser> GetUserById(string Id);
    }
}
