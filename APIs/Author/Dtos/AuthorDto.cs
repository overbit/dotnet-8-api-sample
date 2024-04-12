namespace MyService.APIs.Dtos;

public class AuthorDto : AuthorIdDto
{
    public string? Name { get; set; }

    public IEnumerable<TodoItemIdDto>? TodoItemIds { get; set; }
}
