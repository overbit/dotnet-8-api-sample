using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;


public class TodoItemsService : ITodoItemsService
{
    private readonly MyServiceContext _context;

    public TodoItemsService(MyServiceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItemDto>> TodoItems()
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

    public async Task<TodoItemDto> TodoItem(long id)
    {
        var todo = await _context.TodoItems.FindAsync(id);

        if (todo == null)
        {
            throw new NotFoundException();
        }

        return new TodoItemDto
        {
            Id = todo.Id,
            Name = todo.Name,
            IsComplete = todo.IsComplete
        };
    }

    public async Task UpdateTodoItem(long id, TodoItemDto dto)
    {
        var todo = new TodoItem()
        {
            Id = dto.Id,
            Name = dto.Name,
            IsComplete = dto.IsComplete
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
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    private bool TodoItemExists(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<TodoItemDto> CreateTodoItem(TodoItemDto dto, long workspaceId)
    {
        var todo = new TodoItem()
        {
            Id = dto.Id,
            Name = dto.Name,
            IsComplete = dto.IsComplete,
            WorkspaceId = workspaceId
        };
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

    }

    public async Task<IEnumerable<AuthorDto>> Authors(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        return todoItem.Authors.Select(
            author => new AuthorDto
            {
                Id = author.Id,
                Name = author.Name
            }).ToList();
    }

    public async Task ConnectAuthor(long id, [Required] long authorId)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var author = await _context.Authors.FindAsync(authorId);
        if (author == null)
        {
            throw new NotFoundException();
        }

        todoItem.Authors.Add(author);
        await _context.SaveChangesAsync();

    }

    public async Task DisconnectAuthor(long id, [Required] long authorId)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var author = await _context.Authors.FindAsync(authorId);
        if (author == null)
        {
            throw new NotFoundException();
        }

        todoItem.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
}
