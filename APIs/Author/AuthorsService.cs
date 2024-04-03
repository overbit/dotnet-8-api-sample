using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyService.APIs.Author.Dtos;
using MyService.APIs.Errors;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;

public class AuthorsService : IAuthorsService
{
    private readonly MyServiceContext _context;

    public AuthorsService(MyServiceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorDto>> Authors()
    {
        var authors = await _context.Authors.ToListAsync();
        return authors.ConvertAll(
            author => new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
            });
    }

    public async Task<AuthorDto> Author(long id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            throw new NotFoundException();
        }

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }

    public async Task UpdateAuthor(long id, AuthorDto authorDto)
    {
        var author = new Author
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
        };

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<AuthorDto> CreateAuthor(AuthorDto authorDto)
    {
        var author = new Author
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
        };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return authorDto;
    }

    public async Task DeleteAuthor(long id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodoItem>> TodoItems(long id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        return author.TodoItems.ToList();
    }

    public async Task ConnectTodoItem(long id, [Required] long todoItemId)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var todo = await _context.TodoItems.FindAsync(todoItemId);
        if (todo == null)
        {
            throw new NotFoundException();
        }

        author.TodoItems.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectTodoItem(long id, [Required] long todoItemId)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var todo = await _context.TodoItems.FindAsync(todoItemId);
        if (todo == null)
        {
            throw new NotFoundException();
        }

        author.TodoItems.Remove(todo);
        await _context.SaveChangesAsync();
    }

    private bool AuthorExists(long id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }
}
