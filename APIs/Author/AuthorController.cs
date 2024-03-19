using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyService.APIs.Author.Dtos;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Author
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly MyServiceContext _context;

        public AuthorController(MyServiceContext context)
        {
            _context = context;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetTodoItems()
        {
            var todos = await _context.Authors.ToListAsync();
            return todos.ConvertAll(
                author => new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name,
                });
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetTodoItem(long id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
            };
        }

        // PUT: api/Author/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, AuthorDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            var author = new TodoItem()
            {
                Id = todoItemDto.Id,
                Name = todoItemDto.Name,
            };

            _context.Entry(author).State = EntityState.Modified;

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

        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostTodoItem(AuthorDto todoItemDto, long workspaceId)
        {
            var author = new Infrastructure.Models.Author()
            {
                Id = todoItemDto.Id,
                Name = todoItemDto.Name,
            };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDto.Id }, todoItemDto);
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #region Relationships

        [HttpGet("{id}/todoItems")]
        public async Task<IActionResult> GetTodoItems(long id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var authors = author.TodoItems.ToList();
            return Ok(authors);
        }

        [HttpPost("{id}/todoItems")]
        public async Task<IActionResult> ConnectAuthors(long id, [Required] long todoItemId)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var todo = await _context.TodoItems.FindAsync(todoItemId);
            if (todo == null)
            {
                return NotFound();
            }

            author.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}/todoItems")]
        public async Task<IActionResult> DisconnectAuthors(long id, [Required] long todoItemId)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var todo = await _context.TodoItems.FindAsync(todoItemId);
            if (todo == null)
            {
                return NotFound();
            }

            author.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool TodoItemExists(long id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
