using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;

namespace MyService.APIs;

[Route("api/[controller]")]
[ApiController]
public abstract class WorkspacesControllerBase : ControllerBase
{
    protected readonly IWorkspacesService _service;

    public WorkspacesControllerBase(IWorkspacesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceDto>>> Workspaces(
    [FromQuery] WorkspaceFindMany filter
)
    {
        return Ok(await _service.Workspaces(filter));
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<WorkspaceDto>> Workspace([FromRoute] WorkspaceIdDto idDto)
    {
        try
        {
            return await _service.Workspace(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{Id}")]
    public async Task<IActionResult> UpdateWorkspace(
        [FromRoute] WorkspaceIdDto idDto,
        [FromQuery] WorkspaceUpdateInput authorUpdateDto
    )
    {
        try
        {
            await _service.UpdateWorkspace(idDto, authorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkspaceDto>> CreateWorkspace(WorkspaceCreateInput input)
    {
        var author = await _service.CreateWorkspace(input);

        return CreatedAtAction(nameof(Workspace), new { id = author.Id }, author);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteTodoItem([FromRoute] WorkspaceIdDto idDto)
    {
        try
        {
            await _service.DeleteWorkspace(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get all TodoItems of an Workspace
    /// </summary>
    /// <returns></returns>
    [HttpGet("{Id}/todoItems")]
    public async Task<IActionResult> TodoItems(
        [FromRoute] WorkspaceIdDto idDto,
        [FromQuery] TodoItemFindMany filter
    )
    {
        try
        {
            return Ok(await _service.TodoItems(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect TodoItems to an Workspace
    /// </summary>
    [HttpPost("{Id}/todoItems")]
    public async Task<IActionResult> ConnectTodoItems(
        [FromRoute] WorkspaceIdDto idDto,
        [FromBody] TodoItemIdDto[] todoItemIds
    )
    {
        try
        {
            await _service.ConnectTodoItems(idDto, todoItemIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update all TodoItems of an Workspace
    /// </summary>
    [HttpPatch("{Id}/todoItems")]
    public async Task<IActionResult> UpdateTodoItems(
        [FromRoute] WorkspaceIdDto idDto,
        [FromBody] TodoItemIdDto[] todoItemIds
    )
    {
        try
        {
            await _service.UpdateTodoItems(idDto, todoItemIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect TodoItems from an Workspace
    /// </summary>
    [HttpDelete("{Id}/todoItems")]
    public async Task<IActionResult> DisconnectTodoItems(
        [FromRoute] WorkspaceIdDto idDto,
        [FromBody] TodoItemIdDto[] todoItemIds
    )
    {
        try
        {
            await _service.DisconnectTodoItems(idDto, todoItemIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
