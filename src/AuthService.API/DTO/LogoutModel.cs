using System.ComponentModel.DataAnnotations;
namespace AuthService.API.DTO;


public class LogoutModel
{
    [Required]
    public int UserId { get; set; }
}