using Microsoft.AspNetCore.Identity;

namespace AuthService.Model
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
