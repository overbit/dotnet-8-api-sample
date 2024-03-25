using MyService.APIs.Todo.Dtos;

namespace MyService.APIs.Author.Dtos;

public class AuthorDto
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public ICollection<TodoItemDto>? TodoItems { get; set; }
}