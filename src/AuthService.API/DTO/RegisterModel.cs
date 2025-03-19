using System.ComponentModel.DataAnnotations;

namespace AuthService.API.DTO
{
    public class RegisterModel
    {
        [Required]
        [MinLength(3)]
        public required string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
        
        [Required]
        [MinLength(2)]
        public required string FirstName { get; set; }
        
        [Required]
        [MinLength(2)]
        public required string LastName { get; set; }

    }
}