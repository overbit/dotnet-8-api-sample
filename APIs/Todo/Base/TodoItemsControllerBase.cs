using System.ComponentModel.DataAnnotations;
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

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> TodoItems()
    {
        return Ok(await _service.TodoItems());
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> TodoItem(long id)
    {
        try
        {
            return await _service.TodoItem(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDto)
    {
        if (id != todoItemDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateTodoItem(id, todoItemDto);
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
    public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemCreateInput input)
    {
        var dto = await _service.CreateTodoItem(input);
        return CreatedAtAction(nameof(TodoItem), new { id = dto.Id }, dto);
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
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Authors(long id)
    {
        var authors = await _service.Authors(id);
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
