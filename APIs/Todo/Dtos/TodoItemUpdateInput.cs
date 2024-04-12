namespace MyService.APIs.Dtos;

// Equal to TodoItemCreateInput but with all properties optional
public class TodoItemUpdateInput
{
    public string? Name { get; set; }
    public bool? IsComplete { get; set; }
    public long? workspaceId { get; set; }

    public ICollection<AuthorIdDto>? AuthorIds { get; set; }
}
