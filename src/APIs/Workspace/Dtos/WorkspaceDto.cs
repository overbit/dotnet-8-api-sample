using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class WorkspaceDto : WorkspaceIdDto
{
    [StringLength(250)]
    public string? Name { get; set; }

    public IEnumerable<TodoItemIdDto>? TodoItemIds { get; set; }
}
