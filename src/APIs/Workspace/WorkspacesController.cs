using Microsoft.AspNetCore.Mvc;

namespace MyService.APIs;

[ApiController]
public class WorkspacesController : WorkspacesControllerBase
{
    public WorkspacesController(IWorkspacesService service)
        : base(service) { }
}
