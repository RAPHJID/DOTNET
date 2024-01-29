using AuthMicroService.Model.Dtos;

namespace AuthMicroService.Services.IServices
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto userDto, string role);
        Task<LoginResponseDto> LoginUser(LoginRequestDto loginDto);
        Task<bool> AssignUserRoles(string Email, string RoleName);
    }
}
