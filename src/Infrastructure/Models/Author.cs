using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyService.Infrastructure.Models;

[Table("Authors")]
public class Author
{
    [Key, Required]
    [Range(1, long.MaxValue)]
    public long Id { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>(); // Many-to-Many
}
