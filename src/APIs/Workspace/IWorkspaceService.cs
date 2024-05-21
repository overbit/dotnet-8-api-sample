using MyService.APIs.Dtos;

namespace MyService.APIs;

public interface IWorkspacesService
{
    public Task<IEnumerable<WorkspaceDto>> Workspaces(WorkspaceFindMany findManyArgs);

    public Task<WorkspaceDto> Workspace(WorkspaceIdDto idDto);

    public Task UpdateWorkspace(WorkspaceIdDto idDto, WorkspaceUpdateInput updateInput);

    public Task<WorkspaceDto> CreateWorkspace(WorkspaceCreateInput authorDto);

    public Task DeleteWorkspace(WorkspaceIdDto idDto);

    public Task<IEnumerable<TodoItemDto>> FindTodoItems(
        WorkspaceIdDto idDto,
        TodoItemFindMany todoItemFindMany
    );

    public Task ConnectTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId);

    public Task UpdateTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId);

    public Task DisconnectTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId);
}
