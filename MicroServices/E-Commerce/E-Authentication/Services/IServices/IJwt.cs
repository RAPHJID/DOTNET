using E_Authentication.Models;

namespace E_Authentication.Services.IServices
{
    public interface IJwt
    {
        string GenerateToken(AppUser user, IEnumerable<string> Roles);
    }
}
