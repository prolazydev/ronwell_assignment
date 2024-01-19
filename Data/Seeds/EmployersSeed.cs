using Microsoft.AspNetCore.Identity;
using Assignment.Data;
using Assignment.Models;

namespace SchoolManagementProject.Data.Seed;

public static class EmployersSeed
{
    public static async Task SeedUsersAsync(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var seedStatus = context.SeedStatus.FirstOrDefault();
        if (seedStatus != null)
            return;

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // TODO: finnish
        var superAdmin = new ApplicationUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true,
            Salary = 1000,
            Position = "System Administrator",
        };
        var ceo = new ApplicationUser
        {
            UserName = "ceo@example.com",
            Email = "ceo@example.com",
            EmailConfirmed = true,
            Salary = 15000,
            Position = "CEO",
        };
        var manager = new ApplicationUser
        {
            UserName = "manager@example.com",
            Email = "manager@example.com",
            EmailConfirmed = true,
            Salary = 7500,
            Position = "Manager",
        };

        // Create a quick list of employers
        var r = new Random();
        for (int i = 1; i <= 5; i++)
        {
            var employer = new ApplicationUser
            {
                UserName = $"employer{i}@example.com",
                Email = $"employer{i}@example.com",
                EmailConfirmed = true,
                Salary = Math.Round(r.NextDouble() * (750 - 400) + 400, 2), // Random salary between 400 and 750
                Position = "Employer",
            };

            await SeedUserAsync(userManager, roleManager, employer, "Employer", "Employer123");
        }

        await SeedUserAsync(userManager, roleManager, superAdmin, "SuperAdmin", "Admin123");
        await SeedUserAsync(userManager, roleManager, ceo, "Admin", "Admin123");
        await SeedUserAsync(userManager, roleManager, manager, "Admin", "manager123");

        // Avoid redoing the seeding...
        context.SeedStatus.Add(new Seeds { IsSeedDataApplied = true });
        await context.SaveChangesAsync();
    }

    private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationUser user, string roleName, string password)
    {
        if (userManager.FindByNameAsync(user.UserName!).Result == null)
        {
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Check if the role exists, and create it if not
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleResult = await roleManager.CreateAsync(role);

                    if (!roleResult.Succeeded)
                        throw new Exception($"Error creating role: {string.Join(", ", roleResult.Errors)}");
                }

                await userManager.AddToRoleAsync(user, role.Name!);
            }
        }
        else
            Console.WriteLine($"User {user.UserName} already exists");
    }
}
