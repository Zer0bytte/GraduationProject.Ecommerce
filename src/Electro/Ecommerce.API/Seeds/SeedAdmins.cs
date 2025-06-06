using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Seeds;

public class SeedAdmins
{

    public static async Task Seed(UserManager<AppUser> userManager)
    {
        var users = userManager.Users.ToList();

        foreach (var user in users)
        {
            // Generate password reset token
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            // Reset the password
            var result = await userManager.ResetPasswordAsync(user, resetToken, "Password@123");

            if (result.Succeeded)
            {
                Console.WriteLine($"Password reset for user: {user.UserName}");
            }
            else
            {
                Console.WriteLine($"Failed to reset password for user: {user.UserName}. Errors:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }


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
        string[] usersEmails = ["user1@electro.com", "user2@electro.com"];
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