using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Sneat.PL.Helper
{
    public static class AdminPermmisionList
    {
        public static async Task SeedClaimsForAdminUser(this RoleManager<IdentityRole> roleManager)
        {

            var AdminRole = await roleManager.Roles.Where(r => r.Name == "Admin").FirstOrDefaultAsync();
            await roleManager.AddPermmisionClaimes(AdminRole, "Product");
        }
        public static async Task AddPermmisionClaimes(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaimes = await roleManager.GetClaimsAsync(role);
            var allPermmision = Permmisions.GeneratePermmisionList(module);
            foreach (var Permission in allPermmision)
            {
                if (!allClaimes.Any(c => c.Type == "Permission" && c.Value == Permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", Permission));
            }
        }
    }
}
