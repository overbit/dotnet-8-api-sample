using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;

[Route("api/[controller]")]
[ApiController]
public class WorkspacesControllerBase : ControllerBase
{
    protected readonly IWorkspacesService _service;

    public WorkspacesControllerBase(IWorkspacesService service)
    {
        _service = service;
    }

    // GET: api/Workspaces
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceDto>>> Workspaces()
    {
        var workspaces = await _service.Workspaces();

        return Ok(workspaces);
    }

    // GET: api/Workspaces/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkspaceDto>> Workspace(long id)
    {
        return await _service.Workspace(id);
    }

    // PUT: api/Workspaces/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateWorkspace(long id, WorkspaceDto workspaceDto)
    {
        if (id != workspaceDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateWorkspace(id, workspaceDto);
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
        var dto = await _service.CreateWorkspace(workspaceDto);

        return CreatedAtAction(nameof(Workspace), new { id = dto.Id }, dto);
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
