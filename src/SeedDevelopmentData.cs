using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyService.Infrastructure;

namespace MyService;

public class SeedDevelopmentData
{
    private static string devEmail = "dev@email.local";
    private static string devPassword = "1Password!";

    public static async Task SeedDevUser(
        IServiceProvider serviceProvider,
        IConfiguration configuration
    )
    {
        var context = serviceProvider.GetRequiredService<MyServiceContext>();
        var amplicationRoles = configuration
            .GetSection("AmplicationRoles")
            .AsEnumerable()
            .Where(x => x.Value != null)
            .Select(x => x.Value?.ToString())
            .ToArray();

        var user = new User
        {
            UserName = devEmail,
            NormalizedUserName = devEmail.ToUpperInvariant(),
            Email = devEmail,
            NormalizedEmail = devEmail.ToUpperInvariant(),
            EmailConfirmed = true,
            LockoutEnabled = false,
        };

        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var ca = await userManager.CreateAsync(user);
            var ap = await userManager.AddPasswordAsync(user, devPassword);

            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in amplicationRoles)
            {
                await userManager.AddToRoleAsync(user, _roleManager.NormalizeKey(role));
            }
        }
    }
}
