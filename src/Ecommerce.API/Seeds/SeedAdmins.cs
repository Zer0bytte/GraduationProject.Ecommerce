using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Seeds;

public class SeedAdmins
{

    public static async Task Seed(UserManager<AppUser> userManager)
    {

        if (userManager.Users.Count() == 0)
        {
            AppUser user = new AppUser
            {
                Email = "admin@gradecom.com",
                UserName = "admin@gradecom.com",
                FullName = "Admin"
            };
            IdentityResult result = await userManager.CreateAsync(user, "Password@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
