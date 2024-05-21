using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyService.Infrastructure.Models;

[Table("Tags")]
public class Tag
{
    [Key]
    [Range(1, long.MaxValue)]
    public long Id { get; set; }

    public string? Value { get; set; }

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>(); // Many-to-Many
}
