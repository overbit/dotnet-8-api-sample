using System.ComponentModel.DataAnnotations;
using MyService.APIs.Author.Dtos;
using MyService.APIs.Todo.Dtos;


public interface ITodoItemsService
{
    public Task<IEnumerable<TodoItemDto>> GetTodoItems();

    public Task<TodoItemDto> GetTodoItem(long id);

    public Task PutTodoItem(long id, TodoItemDto dto);

    public Task<TodoItemDto> PostTodoItem(TodoItemDto dto, long workspaceId);

    public Task DeleteTodoItem(long id);

    public Task<IEnumerable<AuthorDto>> GetAuthors(long id);

    public Task ConnectAuthor(long id, [Required] long authorId);

    public Task DisconnectAuthor(long id, [Required] long authorId);
}
