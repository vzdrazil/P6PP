using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [EmailAddress]
        public required string Email { get; set; }
        public ICollection<TokenBlackList> TokenBlackLists { get; set; } = new List<TokenBlackList>();
    }
}