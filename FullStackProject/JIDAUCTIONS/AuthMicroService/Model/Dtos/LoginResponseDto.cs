namespace AuthMicroService.Model.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = default!;
        public string? ErrorMessage { get; set; }
    }
}
