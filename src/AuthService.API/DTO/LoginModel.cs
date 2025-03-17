using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthService.API.DTO
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [SwaggerSchema(Description = "E-mail uživatele")]
        public string Email { get; set; }
        
        [Required]
        [SwaggerSchema(Description = "Heslo uživatele")]
        public string Password { get; set; } 
    }
}