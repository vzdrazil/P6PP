using Microsoft.AspNetCore.Identity;

namespace AuthService.API.Models
{
    public class Role : IdentityRole
    {
        public string Name { get; set; }
    }
}