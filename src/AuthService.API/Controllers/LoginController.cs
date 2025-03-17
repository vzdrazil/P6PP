using AuthService.API.DTO;
using AuthService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.API.Controllers
{
    [Route("api/login")]
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            // Find user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            // Check if the password is correct
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return Unauthorized("Invalid email or password.");

            // Create the JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Přidání role jako claim, pokud role není null
            if (user.Role != null && !string.IsNullOrEmpty(user.Role.Name))
            {
                claims.Add(new Claim("role", user.Role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e1ec5a67c31e5e3d36b59c7f00478432c998c5f524bf3c9ad75e221c48a729d0fea2704fdcc8571390945d05843cec8e7a5303b766a06e92c88f34330890d9ee82db2d2e48b61d3d645aa270cf45fdf2fd22080fd1d3b7603bc0a3d4b77f6eb3bac50d5abe4897a093fa821b5561cdf65fd1f872b0165f283390d8ad0201bc02e69b569b3a2ede792e3310e3d6d967d87a0e00954f01cc1391e3466d03144489a12bbc119f73acef92fb5da06880522b9582a3a08c797aeab1a008e2c1d6a423768966028f7c40c0d07faf7f9b3c57e5abc28582f87de2b7760219a7380d8992669f7c6be0a4ab1eb26018fa9653a9c198d3abec9fdbebbfe18559933e9fbdf5"));
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
}