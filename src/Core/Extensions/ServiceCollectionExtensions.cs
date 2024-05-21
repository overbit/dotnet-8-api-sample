using MyService.APIs;

namespace MyService;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddScoped<IAuthorsService, AuthorsService>();
        services.AddScoped<ITodoItemsService, TodoItemsService>();
        services.AddScoped<IWorkspacesService, WorkspacesService>();
    }
}
