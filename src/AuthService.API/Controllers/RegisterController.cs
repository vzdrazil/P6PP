using AuthService.API.DTO;
using AuthService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Shared.Results;

namespace AuthService.API.Controllers;

[Route("api")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public RegisterController(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
            return BadRequest(new ApiResult<object>(null, false, "User with this email already exists."));
        


        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.UserName,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(new ApiResult<object>(result.Errors, false, result.Errors.ToString()));


        await _userManager.AddToRoleAsync(user, "User");

        //TODO: při registraci se nevyplní normalizedEmail! díky tomu nefunguje login pro email
        return Ok(new ApiResult<RegisterResponse> (new RegisterResponse(user.Id, user.Email)));
    }

    public class RegisterResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public RegisterResponse(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}