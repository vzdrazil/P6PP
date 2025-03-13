using AuthService.API.DTOs;
using AuthService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[Route("api/register")]
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

    [HttpPost("create")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
            return BadRequest("User with this email already exists");

        Role? role = null;
        if (!string.IsNullOrEmpty(model.RoleName))
        {
            role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
                return BadRequest("Invalid role");
        }

        if (!Enum.TryParse<UserState>(model.State, true, out var state))
            return BadRequest("Invalid state");


        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Verified = model.Verified,
            State = state,
            Sex = model.Sex,
            Height = model.Height,
            Weight = model.Weight,
            BirthDay = model.BirthDay,
            RoleId = role.Id,
            Role = role,
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, role.Name);

        return Ok(new
        {
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.RoleId
        });
    }
}