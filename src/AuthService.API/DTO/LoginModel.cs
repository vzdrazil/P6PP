using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthService.API.DTO
{
    public class LoginModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}