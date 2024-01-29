using AuthService.Model;

namespace AuthService.Services.IServices
{
    public interface IJwt
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
