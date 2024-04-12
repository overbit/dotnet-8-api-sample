using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyService.Infrastructure.Models;

[Table("Workspaces")]
public class Workspace
{
    [Key, Required]
    public long Id { get; set; }
    public string? Name { get; set; }

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>(); // one-to-many relationship
}
