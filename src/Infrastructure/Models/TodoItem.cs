using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyService.Infrastructure.Models;

[Table("TodoItems")]
public class TodoItem
{
    [Key]
    [Range(1, long.MaxValue)]
    public long Id { get; set; }
    [StringLength(250)]
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    // Required one-to-many relationship
    public long WorkspaceId { get; set; } // Foreign Key of one-to-many relationship

    [ForeignKey(nameof(WorkspaceId))]
    public Workspace Workspace { get; set; } = null!; // one-to-many relationship

    public long? TagId { get; set; } // Foreign Key of one-to-many relationship

    [ForeignKey(nameof(TagId))]
    public Tag? Tag { get; set; } // one-to-many relationship

    public ICollection<Author> Authors { get; set; } = new List<Author>(); // Many-to-Many
}
