using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)] public required string FirstName { get; set; }

        [MaxLength(100)] public required string LastName { get; set; }

        public bool Verified { get; set; }
        
        public UserState State { get; set; } = UserState.Pending;

        [MaxLength(10)] public required string Sex { get; set; }

        [Column(TypeName = "double")] public required double Height { get; set; }

        [Column(TypeName = "double")] public required double Weight { get; set; }

        public DateTime LastLoggedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime BirthDay { get; set; }

        // Foreign key na Role
        public string? RoleId { get; set; }
        public Role? Role { get; set; }

        // Vztah na TokenBlackList
        public ICollection<TokenBlackList> TokenBlackLists { get; set; } = new List<TokenBlackList>();
    }
}