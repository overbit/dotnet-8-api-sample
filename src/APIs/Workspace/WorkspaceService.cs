using MyService.Infrastructure;

namespace MyService.APIs;

public class WorkspacesService : WorkspacesServiceBase
{
    public WorkspacesService(MyServiceContext context)
        : base(context) { }
}
