using Microsoft.EntityFrameworkCore;
using MyService.APIs.Common;
using MyService.APIs.Dtos;
using MyService.APIs.Errors;
using MyService.APIs.Extensions;
using MyService.Infrastructure;
using MyService.Infrastructure.Models;
using NuGet.Packaging;

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
        var authors = await _context
            .Authors.Include(x => x.TodoItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return authors.ConvertAll(author => author.ToDto());
    }

    public async Task<AuthorDto> Author(AuthorIdDto idDto)
    {
        var authors = await this.Authors(
            new AuthorFindMany { Where = new AuthorWhereInput { Id = idDto.Id } }
        );
        var author = authors.FirstOrDefault();
        if (author == null)
        {
            throw new NotFoundException();
        }

        return author;
    }

    public async Task UpdateAuthor(AuthorIdDto idDto, AuthorUpdateInput updateDto)
    {
        var author = updateDto.ToModel(idDto);

        if (updateDto.TodoItemIds != null)
        {
            author.TodoItems = await _context
                .TodoItems.Where(todo => updateDto.TodoItemIds.Select(t => t.Id).Contains(todo.Id))
                .ToListAsync();
        }

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(idDto))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<AuthorDto> CreateAuthor(AuthorCreateInput createDto)
    {
        var model = new Author { Name = createDto.Name, };
        if (createDto.Id != null)
        {
            model.Id = createDto.Id.Value;
        }

        if (createDto.TodoItemIds != null)
        {
            model.TodoItems = await _context
                .TodoItems.Where(todo => createDto.TodoItemIds.Select(t => t.Id).Contains(todo.Id))
                .ToListAsync();
        }

        _context.Authors.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Author>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteAuthor(AuthorIdDto idDto)
    {
        var author = await _context.Authors.FindAsync(idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodoItemDto>> FindTodoItems(
        AuthorIdDto idDto,
        TodoItemFindMany todoItemFindMany
    )
    {
        var todoItems = await _context
            .TodoItems.Where(a => a.Authors.Any(t => t.Id == idDto.Id))
            .ApplyWhere(todoItemFindMany.Where)
            .ApplySkip(todoItemFindMany.Skip)
            .ApplyTake(todoItemFindMany.Take)
            .ApplyOrderBy(todoItemFindMany.SortBy)
            .ToListAsync();

        return todoItems.Select(x => x.ToDto());
    }

    public async Task ConnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var author = await _context
            .Authors.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(t => todoItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (todoItems.Count == 0)
        {
            throw new NotFoundException();
        }

        var newTodoItems = todoItems.Except(author.TodoItems);
        author.TodoItems.AddRange(newTodoItems);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var author = await _context
            .Authors.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(t => todoItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (todoItems.Count == 0)
        {
            throw new NotFoundException();
        }

        author.TodoItems = todoItems;
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var author = await _context
            .Authors.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (author == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(t => todoItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var todoItem in todoItems)
        {
            author.TodoItems.Remove(todoItem);
        }
        await _context.SaveChangesAsync();
    }

    private bool AuthorExists(AuthorIdDto idDto)
    {
        return _context.Authors.Any(e => e.Id == idDto.Id);
    }
}
