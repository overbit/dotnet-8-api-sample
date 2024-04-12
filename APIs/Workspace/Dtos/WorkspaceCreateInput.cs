namespace MyService.APIs.Dtos;

public class WorkspaceCreateInput
{
    public long? Id { get; set; }
    public string? Name { get; set; }

    public ICollection<TodoItemIdDto>? TodoItemIds { get; set; }
}
