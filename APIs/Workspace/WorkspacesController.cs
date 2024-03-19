using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Errors;
using MyService.APIs.Workspace.Dtos;


[Route("api/[controller]")]
[ApiController]
public class WorkspacesController : ControllerBase
{
    private readonly IWorkspacesService _service;

    public WorkspacesController(IWorkspacesService service)
    {
        _service = service;
    }

    // GET: api/Workspaces
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceDto>>> GetWorkspaces()
    {
        var workspaces = await _service.GetWorkspaces();

        return Ok(workspaces);
    }

    // GET: api/Workspaces/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkspaceDto>> GetWorkspace(long id)
    {
        return await _service.GetWorkspace(id);
    }

    // PUT: api/Workspaces/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkspace(long id, WorkspaceDto workspaceDto)
    {
        if (id != workspaceDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.PutWorkspace(id, workspaceDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Workspaces
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkspaceDto>> PostWorkspace(WorkspaceDto workspaceDto)
    {
        var dto = await _service.PostWorkspace(workspaceDto);

        return CreatedAtAction(nameof(GetWorkspace), new { id = dto.Id }, dto);
    }

    // DELETE: api/Workspaces/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkspace(long id)
    {
        try
        {
            await _service.DeleteWorkspace(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
