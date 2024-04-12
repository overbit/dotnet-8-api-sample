namespace MyService.APIs.Dtos;

public class TodoItemWhereInput
{
    public long? Id { get; set; }
    public string? Name { get; set; }

    public bool? IsComplete { get; set; }

    public long? workspaceId { get; set; }
}
