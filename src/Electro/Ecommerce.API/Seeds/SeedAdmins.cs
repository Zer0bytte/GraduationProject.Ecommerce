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
                Email = "admin@electro.com",
                UserName = "admin@electro.com",
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


public class SeedUsers
{

    public static async Task Seed(UserManager<AppUser> userManager)
    {
        string[] usersEmails = [
                "user1@electro.com",
                "user2@electro.com",
                "user3@electro.com",
                "user4@electro.com",
                "user5@electro.com",
            ];
        foreach (var email in usersEmails)
        {
            if (!userManager.Users.Where(u => u.Email == email).Any())
            {
                AppUser user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    FullName = "تيست يوزر",
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(user, "Password@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}