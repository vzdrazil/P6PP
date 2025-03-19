using AuthService.API.DTO;
using AuthService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Shared;
using ReservationSystem.Shared.Clients;
using ReservationSystem.Shared.Results;

namespace AuthService.API.Controllers;

[Route("api")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly NetworkHttpClient _httpClient;
    // private readonly RoleManager<Role> _roleManager;

    public RegisterController(UserManager<ApplicationUser> userManager, NetworkHttpClient httpClient)
    {
        _userManager = userManager;
        _httpClient = httpClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
            return BadRequest(new ApiResult<object>(null, false, "User with this email already exists."));
        
        // Here call the user service to create a user
        var url = ServiceEndpoints.UserService.CreateUser;
        var newUser = new
        {
            Username = model.UserName,
            FirstName = model.UserName,
            LastName = model.UserName,
            Email = model.Email,
        };
        
        var response = await _httpClient.PostAsync<object, object>(url, newUser, CancellationToken.None);
        
        if (!response.Success)
            return BadRequest(new ApiResult<object>(null, false, response.Message));
        
        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.UserName,
            UserId = int.Parse(response.Data.ToString())
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(new ApiResult<object>(result.Errors, false, result.Errors.ToString()));
        
        //TODO: při registraci se nevyplní normalizedEmail! díky tomu nefunguje login pro email
        return Ok(new ApiResult<RegisterResponse> (new RegisterResponse(user.UserId, user.Email)));
    }

    public class RegisterResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public RegisterResponse(int id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}