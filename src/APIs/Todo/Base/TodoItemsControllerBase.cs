using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;

namespace MyService.APIs;

[Route("api/[controller]")]
[ApiController]
public abstract class TodoItemsControllerBase : ControllerBase
{
    protected readonly ITodoItemsService _service;

    public TodoItemsControllerBase(ITodoItemsService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> TodoItems(
        [FromQuery] TodoItemFindMany findManyDto
    )
    {
        return Ok(await _service.TodoItems(findManyDto));
    }

    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<TodoItemDto>> TodoItem([FromRoute] TodoItemIdDto idDto)
    {
        try
        {
            return await _service.TodoItem(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> UpdateTodoItem(
        [FromRoute] TodoItemIdDto idDto,
        [FromBody] TodoItemUpdateInput updateDto
    )
    {
        try
        {
            await _service.UpdateTodoItem(idDto, updateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemCreateInput input)
    {
        var dto = await _service.CreateTodoItem(input);
        return CreatedAtAction(nameof(TodoItem), new { id = dto.Id }, dto);
    }

    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> DeleteTodoItem([FromRoute] TodoItemIdDto idDto)
    {
        try
        {
            await _service.DeleteTodoItem(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{Id}/workspace")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WorkspaceDto>> GetWorkspace([FromRoute] TodoItemIdDto idDto)
    {
        var workspace = await _service.GetWorkspace(idDto);
        return Ok(workspace);
    }

    [HttpGet("{Id}/authors")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Authors(
        [FromRoute] TodoItemIdDto idDto,
        [FromQuery] AuthorFindMany filter
    )
    {
        var authors = await _service.FindAuthors(idDto, filter);
        return Ok(authors);
    }

    [HttpPost("{Id}/authors")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> ConnectAuthors(
        [FromRoute] TodoItemIdDto idDto,
        [FromBody] AuthorIdDto[] authorIds
    )
    {
        try
        {
            await _service.ConnectAuthors(idDto, authorIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{Id}/authors")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> UpdateAuthors(
        [FromRoute] TodoItemIdDto idDto,
        [FromBody] AuthorIdDto[] authorIds
    )
    {
        try
        {
            await _service.UpdateAuthors(idDto, authorIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{Id}/authors")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> DisconnectAuthors(
        [FromRoute] TodoItemIdDto idDto,
        [FromBody] AuthorIdDto[] authorIds
    )
    {
        try
        {
            await _service.DisconnectAuthors(idDto, authorIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }
}
