using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyService.Infrastructure;

namespace MyService;

public class SeedDevelopmentData
{
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
            .Select(x => x.Value.ToString())
            .ToArray();

        var user = new User
        {
            UserName = "developer",
            NormalizedUserName = "developer",
            Email = "developer@email.local",
            NormalizedEmail = "developer@email.local",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "password");
            user.PasswordHash = hashed;
            var userStore = new UserStore<User>(context);
            await userStore.CreateAsync(user);
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in amplicationRoles)
            {
                await userStore.AddToRoleAsync(user, _roleManager.NormalizeKey(role));
            }
        }

        await context.SaveChangesAsync();
    }
}
