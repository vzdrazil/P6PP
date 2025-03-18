using AuthService.API.DTO;
using AuthService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ReservationSystem.Shared.Results;


namespace AuthService.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResult<object>(null, false, "Invalid data."));

            ApplicationUser user = null;

            if (!string.IsNullOrEmpty(model.Email))
            {
                user = await _userManager.FindByEmailAsync(model.Email);
            }
            else if (!string.IsNullOrEmpty(model.UserName))
            {
                user = await _userManager.FindByNameAsync(model.UserName);
            }

            if (user == null)
                return Unauthorized(new ApiResult<object>(null, false, "Invalid username/email or password."));

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return Unauthorized(new ApiResult<object>(null, false, "Invalid username/email or password."));

            var token = GenerateJwtToken(user);
            return Ok(new ApiResult<TokenResponse> (new TokenResponse(token)));
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
