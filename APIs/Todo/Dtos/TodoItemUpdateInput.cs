namespace MyService.APIs.Dtos;

// Equal to TodoItemCreateInput but with all properties optional
public class TodoItemUpdateInput
{
    public long? workspaceId { get; set; }
    public string? Name { get; set; }
    public bool? IsComplete { get; set; }

    public ICollection<AuthorIdDto>? AuthorIds { get; set; }
}
