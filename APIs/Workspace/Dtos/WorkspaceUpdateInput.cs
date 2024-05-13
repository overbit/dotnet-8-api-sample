using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

// Equal to WorkspaceCreateInput but with all properties optional
public class WorkspaceUpdateInput
{
    [StringLength(250)]
    public string? Name { get; set; }

    public ICollection<TodoItemIdDto>? TodoItemIds { get; set; }
}
