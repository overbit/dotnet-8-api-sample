using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyService.Infrastructure.Models;

[Table("TodoItems")]
public class TodoItem
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    [Required] // Required one-to-many relationship
    public long WorkspaceId { get; set; } // Foreign Key of one-to-many relationship

    [ForeignKey(nameof(WorkspaceId))]
    public Workspace Workspace { get; set; } = null!; // one-to-many relationship

    public ICollection<Author> Authors { get; set; } = new List<Author>(); // Many-to-Many
}
