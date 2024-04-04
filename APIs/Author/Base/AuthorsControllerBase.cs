using System.ComponentModel.DataAnnotations;
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
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Authors()
    {
        return Ok(await _service.Authors());
    }

    // GET: api/author/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> Author(long id)
    {
        try
        {
            return await _service.Author(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // PUT: api/author/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAuthor(long id, AuthorDto authorDto)
    {
        if (id != authorDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateAuthor(id, authorDto);
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        try
        {
            await _service.DeleteAuthor(id);
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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/todoItems")]
    public async Task<IActionResult> TodoItems(long id)
    {
        try
        {
            return Ok(await _service.TodoItems(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect a TodoItem to an Author
    /// </summary>
    /// <param name="id"></param>
    /// <param name="todoItemId"></param>
    /// <returns></returns>
    [HttpPost("{id}/todoItems")]
    public async Task<IActionResult> ConnectTodoItem(long id, [Required] long todoItemId)
    {
        try
        {
            await _service.ConnectTodoItem(id, todoItemId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect a TodoItem from an Author
    /// </summary>
    /// <param name="id"></param>
    /// <param name="todoItemId"></param>
    /// <returns></returns>
    [HttpDelete("{id}/todoItems")]
    public async Task<IActionResult> DisconnectTodoItem(long id, [Required] long todoItemId)
    {
        try
        {
            await _service.DisconnectTodoItem(id, todoItemId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

}
