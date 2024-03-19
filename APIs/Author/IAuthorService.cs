using System.ComponentModel.DataAnnotations;
using MyService.APIs.Author.Dtos;
using MyService.Infrastructure.Models;


public interface IAuthorService
{
    public Task<IEnumerable<AuthorDto>> GetAuthors();

    public Task<AuthorDto> GetAuthor(long id);
    public Task PutAuthor(long id, AuthorDto authorDto);

    public Task<AuthorDto> PostAuthor(AuthorDto authorDto);

    public Task DeleteAuthor(long id);

    public Task<IEnumerable<TodoItem>> GetTodoItems(long id);

    public Task ConnectTodoItem(long id, [Required] long todoItemId);

    public Task DisconnectTodoItem(long id, [Required] long todoItemId);
}