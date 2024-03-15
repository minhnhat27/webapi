using Microsoft.AspNetCore.Identity;
using System.Data;
using webapi.Models;

namespace webapi.Data
{
    public class DataSeeder
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Admin";
                user.Email = "admin@gmail.com";
                user.EmailConfirmed = true;
                user.AccessFailedCount = 0;
                user.Id = "1000";
                user.PhoneNumberConfirmed = true;
                user.TwoFactorEnabled = false;
                user.LockoutEnabled = false;

                userManager.AddToRoleAsync(user, "Admin");

                userManager.CreateAsync(user, "Admin@123");
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roleAdmin = new IdentityRole
            {
                Id = "1",
                Name = "Admin"
            };
            var roleUser = new IdentityRole
            {
                Id = "2",
                Name = "User"
            };
            roleManager.CreateAsync(roleUser);
            roleManager.CreateAsync(roleAdmin);
        }

    }
}
