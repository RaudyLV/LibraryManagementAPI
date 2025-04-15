using Application.Enums;
using identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString() ));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString() ));
        }
    }
}
