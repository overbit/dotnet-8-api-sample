using MyService.APIs.Dtos;

namespace MyService.APIs;

public interface ITodoItemsService
{
    public Task<IEnumerable<TodoItemDto>> TodoItems(TodoItemFindMany findManyArgs);

    public Task<TodoItemDto> TodoItem(TodoItemIdDto idDto);

    public Task UpdateTodoItem(TodoItemIdDto idDto, TodoItemUpdateInput updateDto);

    public Task<TodoItemDto> CreateTodoItem(TodoItemCreateInput createDto);

    public Task DeleteTodoItem(TodoItemIdDto idDto);

    public Task<WorkspaceDto> GetWorkspace(TodoItemIdDto idDto);

    public Task<IEnumerable<AuthorDto>> FindAuthors(
        TodoItemIdDto idDto,
        AuthorFindMany authorFindMany
    );

    public Task ConnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] todoItemsId);

    public Task UpdateAuthors(TodoItemIdDto idDto, AuthorIdDto[] todoItemsId);

    public Task DisconnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] todoItemsId);
}
