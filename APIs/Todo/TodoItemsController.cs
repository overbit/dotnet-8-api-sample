using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyService.APIs.Author.Dtos;
using MyService.APIs.Errors;
using MyService.APIs.Todo.Dtos;


[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoItemsService _service;

    public TodoItemsController(ITodoItemsService service)
    {
        _service = service;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        return Ok(await _service.GetTodoItems());
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        try
        {
            return await _service.GetTodoItem(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoItemDto)
    {
        if (id != todoItemDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.PutTodoItem(id, todoItemDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoItemDto, long workspaceId)
    {
        var dto = await _service.PostTodoItem(todoItemDto, workspaceId);
        return CreatedAtAction(nameof(GetTodoItem), new { id = dto.Id }, dto);
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        try
        {
            await _service.DeleteTodoItem(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }


    [HttpGet("{id}/authors")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors(long id)
    {
        var authors = await _service.GetAuthors(id);
        return Ok(authors);
    }

    [HttpPost("{id}/authors")]
    public async Task<IActionResult> ConnectAuthor(long id, [Required] long authorId)
    {
        try
        {
            await _service.ConnectAuthor(id, authorId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }


    [HttpDelete("{id}/authors")]
    public async Task<IActionResult> DisconnectAuthors(long id, [Required] long authorId)
    {

        try
        {
            await _service.DisconnectAuthor(id, authorId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }
}
