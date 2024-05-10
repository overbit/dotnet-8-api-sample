using MyService.APIs.Dtos;

namespace MyService.APIs;

public interface IAuthorsService
{
    public Task<IEnumerable<AuthorDto>> Authors(AuthorFindMany findManyArgs);

    public Task<AuthorDto> Author(AuthorIdDto idDto);

    public Task UpdateAuthor(AuthorIdDto idDto, AuthorUpdateInput updateInput);

    public Task<AuthorDto> CreateAuthor(AuthorCreateInput authorDto);

    public Task DeleteAuthor(AuthorIdDto idDto);

    public Task<IEnumerable<TodoItemDto>> FindTodoItems(
        AuthorIdDto idDto,
        TodoItemFindMany todoItemFindMany
    );

    public Task ConnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId);

    public Task UpdateTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId);

    public Task DisconnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId);
}
