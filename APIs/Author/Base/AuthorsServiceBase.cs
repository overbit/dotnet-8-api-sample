using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyService.APIs.Dtos;
using MyService.APIs.Extensions;
using MyService.APIs.Errors;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;
using System.IO.Compression;

namespace MyService.APIs;

public abstract class AuthorsServiceBase : IAuthorsService
{
    protected readonly MyServiceContext _context;

    public AuthorsServiceBase(MyServiceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorDto>> Authors(AuthorFindMany findManyArgs)
    {
        var wherePredicate = findManyArgs.ToWherePredicate();

        var authors = await _context.Authors
                                    .Where(wherePredicate)
                                    .ApplySkip(findManyArgs.Skip)
                                    .ApplyTake(findManyArgs.Take)
                                    .ApplyOrderBy(findManyArgs.SortBy)
                                    .ToListAsync();

        return authors.ConvertAll(author => author.ToDto());
    }

    public async Task<AuthorDto> Author(long id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            throw new NotFoundException();
        }

        return author.ToDto();
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

    public async Task<AuthorDto> CreateAuthor(AuthorCreateInput inputDto)
    {
        var model = new Author
        {
            Id = inputDto.Id,
            Name = inputDto.Name,
        };
        _context.Authors.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Author>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
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

    public async Task<IEnumerable<TodoItemDto>> TodoItems(long id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        return author.TodoItems.Select(todo => todo.ToDto()).ToList();
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
