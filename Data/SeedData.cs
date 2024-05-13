using EurofinsEvents.Models;
using Microsoft.AspNetCore.Identity;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        if (_roleManager != null)
        {
            IdentityRole? role = await _roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                var results = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var me = await _userManager.FindByEmailAsync("test@hotmail.com");

            if (me == null)
            {
                //a hasher to hash the password before seeding the user to the db
                var hasher = new PasswordHasher<ApplicationUser>();

                me = new ApplicationUser
                {
                    Id = "13681951-1937-421b-8e0e-6001d8785140", // primary key
                    UserName = "admin@hotmail.com",
                    Email = "admin@hotmail.com",
                    NormalizedEmail = "ADMIN@HOTMAIL.COM",
                    NormalizedUserName = "ADMIN@HOTMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Hello1.")
                   // Qwer@123
                };
                var results = await _userManager.CreateAsync(me);
            }

            await _userManager.AddToRoleAsync(me, "Admin");
        }
    }
}
