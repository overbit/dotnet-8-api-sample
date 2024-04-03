using System.ComponentModel.DataAnnotations;
using MyService.APIs.Author.Dtos;
using MyService.Infrastructure.Models;


public interface IAuthorsService
{
    public Task<IEnumerable<AuthorDto>> Authors();

    public Task<AuthorDto> Author(long id);

    public Task UpdateAuthor(long id, AuthorDto authorDto);

    public Task<AuthorDto> CreateAuthor(AuthorDto authorDto);

    public Task DeleteAuthor(long id);

    public Task<IEnumerable<TodoItem>> TodoItems(long id);

    public Task ConnectTodoItem(long id, [Required] long todoItemId);

    public Task DisconnectTodoItem(long id, [Required] long todoItemId);
}