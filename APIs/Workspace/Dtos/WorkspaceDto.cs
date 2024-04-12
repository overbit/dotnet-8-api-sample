namespace MyService.APIs.Dtos;

public class WorkspaceDto : WorkspaceIdDto
{
    public string? Name { get; set; }

    public IEnumerable<TodoItemIdDto>? TodoItemIds { get; set; }
}
