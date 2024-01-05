using Microsoft.AspNetCore.Identity;
namespace E_Authentication.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }=string.Empty;
        //public int Age { get; set; }

    }
}
