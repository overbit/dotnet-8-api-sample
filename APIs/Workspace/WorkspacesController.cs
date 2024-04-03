using Microsoft.AspNetCore.Mvc;

[ApiController]
public class WorkspacesController : WorkspacesControllerBase
{
    public WorkspacesController(IWorkspacesService service) : base(service) { }
}
