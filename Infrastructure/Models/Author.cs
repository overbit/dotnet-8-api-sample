namespace MyService.Infrastructure.Models;

public class Author
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public ICollection<TodoItem> TodoItems { get; } = new List<TodoItem>();  // Many-to-Many
}