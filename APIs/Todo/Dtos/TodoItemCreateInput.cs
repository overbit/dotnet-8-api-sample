namespace MyService.APIs.Dtos;

public class TodoItemCreateInput
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public long workspaceId { get; set; }

    public ICollection<AuthorDto>? Authors { get; set; }
}
