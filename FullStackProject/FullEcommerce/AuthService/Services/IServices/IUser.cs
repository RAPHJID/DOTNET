using AuthService.Model.Dtos;

namespace AuthService.Services.IServices
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterRequestDto registerRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignUserRole(string email, string Rolename);
    }
}
