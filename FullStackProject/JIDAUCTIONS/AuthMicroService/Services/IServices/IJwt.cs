using AuthMicroService.Model;

namespace AuthMicroService.Services.IServices
{
    public interface IJwt
    {
        string GenerateToken(AppUser user, IEnumerable<string> Roles);
    }
}
