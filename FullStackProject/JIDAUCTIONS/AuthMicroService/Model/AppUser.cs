using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Model
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; } = String.Empty;
    }
}
