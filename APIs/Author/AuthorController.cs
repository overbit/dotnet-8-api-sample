using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Author.Dtos;
using MyService.APIs.Errors;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    // GET: api/author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        return Ok(await _service.GetAuthors());
    }

    // GET: api/author/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(long id)
    {
        try
        {
            return await _service.GetAuthor(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // PUT: api/author/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(long id, AuthorDto authorDto)
    {
        if (id != authorDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.PutAuthor(id, authorDto);
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
    public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorDto authorDto)
    {
        await _service.PostAuthor(authorDto);

        return CreatedAtAction(nameof(GetAuthor), new { id = authorDto.Id }, authorDto);
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
    public async Task<IActionResult> GetTodoItems(long id)
    {
        try
        {
            return Ok(await _service.GetTodoItems(id));
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
