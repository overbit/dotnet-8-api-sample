namespace MyService.Infrastructure.Models;

public class Workspace
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public ICollection<TodoItem> Todos { get; set; } = new List<TodoItem>(); // one-to-many relationship
}