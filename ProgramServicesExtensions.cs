using MyService.APIs;

namespace MyService;

public static class ProgramServicesExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddScoped<IAuthorsService, AuthorsService>();
        services.AddScoped<ITodoItemsService, TodoItemsService>();
        services.AddScoped<IWorkspacesService, WorkspacesService>();
    }
}
