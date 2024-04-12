namespace MyService.APIs.Dtos;

// Equal to AuthorCreateInput but with all properties optional
public class AuthorUpdateInput
{
    public string? Name { get; set; }

    public ICollection<TodoItemIdDto>? TodoItemIds { get; set; }
}
