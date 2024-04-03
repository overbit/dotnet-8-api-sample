using MyService.Infrastructure;

public class WorkspacesService : WorkspacesServiceBase
{
    public WorkspacesService(MyServiceContext context) : base(context) { }
}