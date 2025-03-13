using System.ComponentModel.DataAnnotations;

namespace AuthService.API.DTOs
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; } = null;

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        [Required]
        public required string  FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        //[Required]
        public required string RoleName { get; set; }

        public bool Verified { get; set; } = false;

        public string State { get; set; }
        public required string Sex { get; set; }

        public required double Height { get; set; }
        public required double Weight { get; set; }

        public DateTime BirthDay { get; set; }
    }
}