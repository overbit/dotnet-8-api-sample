using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyService.Infrastructure.Models;
[Table("Authors")]
public class Author
{
    [Key, Required]
    public long Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public ICollection<TodoItem> TodoItems { get; } = new List<TodoItem>();  // Many-to-Many
}