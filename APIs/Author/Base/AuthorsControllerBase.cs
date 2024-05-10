using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;

namespace MyService.APIs;

[Route("api/[controller]")]
[ApiController]
public abstract class AuthorsControllerBase : ControllerBase
{
    protected readonly IAuthorsService _service;

    public AuthorsControllerBase(IAuthorsService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Authors(
        [FromQuery] AuthorFindMany filter
    )
    {
        return Ok(await _service.Authors(filter));
    }

    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<AuthorDto>> Author([FromRoute] AuthorIdDto idDto)
    {
        try
        {
            return await _service.Author(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> UpdateAuthor(
        [FromRoute] AuthorIdDto idDto,
        [FromQuery] AuthorUpdateInput authorUpdateDto
    )
    {
        try
        {
            await _service.UpdateAuthor(idDto, authorUpdateDto);
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
    public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorCreateInput input)
    {
        var author = await _service.CreateAuthor(input);

        return CreatedAtAction(nameof(Author), new { id = author.Id }, author);
    }

    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> DeleteTodoItem([FromRoute] AuthorIdDto idDto)
    {
        try
        {
            await _service.DeleteAuthor(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get all TodoItems of an Author
    /// </summary>
    /// <returns></returns>
    [HttpGet("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> TodoItems(
        [FromRoute] AuthorIdDto idDto,
        [FromQuery] TodoItemFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindTodoItems(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect TodoItems to an Author
    /// </summary>
    [HttpPost("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> ConnectTodoItems(
        [FromRoute] AuthorIdDto idDto,
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
    /// Update all TodoItems of an Author
    /// </summary>
    [HttpPatch("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> UpdateTodoItems(
        [FromRoute] AuthorIdDto idDto,
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
    /// Disconnect TodoItems from an Author
    /// </summary>
    [HttpDelete("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> DisconnectTodoItems(
        [FromRoute] AuthorIdDto idDto,
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
