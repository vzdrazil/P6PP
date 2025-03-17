using Microsoft.AspNetCore.Identity;
using AuthService.API.Models;

namespace AuthService.API.Data
{
    public class RoleSeeder
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleSeeder(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new List<string> { "Admin", "User", "Manager" };

            foreach (var roleName in roles)
            {
                Console.WriteLine($"Test creating role {roleName}");

                if (!string.IsNullOrWhiteSpace(roleName) && !await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        // Log the errors
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error creating role {roleName}: {error.Description}");
                        }
                    }
                }
            }
        }
    }
}