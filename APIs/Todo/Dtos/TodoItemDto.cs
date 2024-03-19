using MyService.APIs.Author.Dtos;

namespace MyService.APIs.Todo.Dtos;

public class TodoItemDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public ICollection<AuthorDto>? Authors { get; set; }
}