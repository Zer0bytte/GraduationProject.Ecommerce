using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Seeds;

public class SeedRoles
{
    public static async Task Seed(RoleManager<IdentityRole<Guid>> roleManager)
    {
        if (await roleManager.FindByNameAsync("Admin") is null)
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "Admin"
            });
            Console.WriteLine();
        }

        if (await roleManager.FindByNameAsync("User") is null)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "User"
            });
        }

        if (await roleManager.FindByNameAsync("Supplier") is null)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "Supplier"
            });
        }
    }
}
