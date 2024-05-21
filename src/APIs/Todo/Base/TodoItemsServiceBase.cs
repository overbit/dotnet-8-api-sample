using Microsoft.EntityFrameworkCore;
using MyService.APIs.Common;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;
using MyService.APIs.Extensions;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;
using NuGet.Packaging;

namespace MyService.APIs;

public abstract class TodoItemsServiceBase : ITodoItemsService
{
    protected readonly MyServiceContext _context;

    public TodoItemsServiceBase(MyServiceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItemDto>> TodoItems(TodoItemFindMany findManyArgs)
    {
        var todos = await _context
            .TodoItems.Include(x => x.Authors)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return todos.ConvertAll(todo => todo.ToDto());
    }

    public async Task<TodoItemDto> TodoItem(TodoItemIdDto idDto)
    {
        var todo = await _context.TodoItems.FindAsync(idDto.Id);

        if (todo == null)
        {
            throw new NotFoundException();
        }

        return todo.ToDto();
    }

    public async Task UpdateTodoItem(TodoItemIdDto idDto, TodoItemUpdateInput updateDto)
    {
        var todoItem = updateDto.ToModel(idDto);

        if (updateDto.AuthorIds != null)
        {
            todoItem.Authors = await _context
                .Authors.Where(todo => updateDto.AuthorIds.Select(t => t.Id).Contains(todo.Id))
                .ToListAsync();
        }

        _context.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(idDto))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<TodoItemDto> CreateTodoItem(TodoItemCreateInput dto)
    {
        var todo = new TodoItem()
        {
            Id = dto.Id,
            Name = dto.Name,
            IsComplete = dto.IsComplete,
            WorkspaceId = dto.workspaceId
        };
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();

        return todo.ToDto();
    }

    public async Task DeleteTodoItem(TodoItemIdDto idDto)
    {
        var todoItem = await _context.TodoItems.FindAsync(idDto.Id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task<WorkspaceDto> GetWorkspace(TodoItemIdDto idDto)
    {
        var todoItem = await _context
            .TodoItems.Where(x => x.Id == idDto.Id)
            .Include(x => x.Workspace)
            .FirstOrDefaultAsync();

        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        return todoItem.Workspace.ToDto();
    }

    public async Task<IEnumerable<AuthorDto>> FindAuthors(
        TodoItemIdDto idDto,
        AuthorFindMany authorFindMany
    )
    {
        var authors = await _context
            .Authors.Where(a => a.TodoItems.Any(t => t.Id == idDto.Id))
            .ApplyWhere(authorFindMany.Where)
            .ApplySkip(authorFindMany.Skip)
            .ApplyTake(authorFindMany.Take)
            .ApplyOrderBy(authorFindMany.SortBy)
            .ToListAsync();

        return authors.Select(x => x.ToDto());
    }

    public async Task ConnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorIdDtos)
    {
        var todoItem = await _context
            .TodoItems.Include(t => t.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(a => authorIdDtos.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (authors.Count == 0)
        {
            throw new NotFoundException();
        }

        var newAuthors = authors.Except(todoItem.Authors);

        todoItem.Authors.AddRange(newAuthors);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorIdDtos)
    {
        var todoItem = await _context
            .TodoItems.Include(t => t.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(a => authorIdDtos.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (authors.Count == 0)
        {
            throw new NotFoundException();
        }

        todoItem.Authors = authors;
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorIdDtos)
    {
        var todoItem = await _context
            .TodoItems.Include(t => t.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(a => authorIdDtos.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        foreach (var author in authors)
        {
            todoItem.Authors.Remove(author);
        }
        await _context.SaveChangesAsync();
    }

    private bool TodoItemExists(TodoItemIdDto idDto)
    {
        return _context.TodoItems.Any(e => e.Id == idDto.Id);
    }
}
