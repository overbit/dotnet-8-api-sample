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

    // GET: api/author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Authors(
        [FromQuery] AuthorFindMany filter
    )
    {
        return Ok(await _service.Authors(filter));
    }

    // GET: api/author/5
    [HttpGet("{Id}")]
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

    // PATCH: api/author/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{Id}")]
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

    // POST: api/author
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorCreateInput input)
    {
        var author = await _service.CreateAuthor(input);

        return CreatedAtAction(nameof(Author), new { id = author.Id }, author);
    }

    // DELETE: api/author/5
    [HttpDelete("{Id}")]
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
    public async Task<IActionResult> TodoItems(
        [FromRoute] AuthorIdDto idDto,
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
    /// Connect TodoItems to an Author
    /// </summary>
    [HttpPost("{Id}/todoItems")]
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
