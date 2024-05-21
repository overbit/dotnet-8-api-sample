using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyService.Infrastructure.Models;

namespace MyService.Infrastructure;

public class MyServiceContext : IdentityDbContext<User>
{
    public MyServiceContext(DbContextOptions<MyServiceContext> options)
        : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<Workspace> Workspaces { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
}
