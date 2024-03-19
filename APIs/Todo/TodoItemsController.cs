using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyService.APIs.Todo.Dtos;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;

namespace MyService.APIs.ToDo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly MyServiceContext _context;

        public TodoItemsController(MyServiceContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var todos = await _context.TodoItems.ToListAsync();
            return todos.ConvertAll(
                todo => new TodoItemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    IsComplete = todo.IsComplete
                });
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todo = await _context.TodoItems.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return new TodoItemDto
            {
                Id = todo.Id,
                Name = todo.Name,
                IsComplete = todo.IsComplete
            };
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

            var todo = new TodoItem()
            {
                Id = todoItemDto.Id,
                Name = todoItemDto.Name,
                IsComplete = todoItemDto.IsComplete
            };

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoItemDto, long workspaceId)
        {
            var todo = new TodoItem()
            {
                Id = todoItemDto.Id,
                Name = todoItemDto.Name,
                IsComplete = todoItemDto.IsComplete
            };
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDto.Id }, todoItemDto);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #region Relationships

        [HttpGet("{id}/authors")]
        public async Task<IActionResult> GetAuthors(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            var authors = todoItem.Authors.ToList();
            return Ok(authors);
        }

        [HttpPost("{id}/authors")]
        public async Task<IActionResult> ConnectAuthors(long id, [Required] long authorId)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }

            todoItem.Authors.Add(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}/authors")]
        public async Task<IActionResult> DisconnectAuthors(long id, [Required] long authorId)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }

            todoItem.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
