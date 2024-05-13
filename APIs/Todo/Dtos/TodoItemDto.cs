using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class TodoItemDto : TodoItemIdDto
{
    [StringLength(250)]
    public string? Name { get; set; }

    public bool IsComplete { get; set; }

    public required WorkspaceIdDto WorkspaceId { get; set; }

    public ICollection<AuthorIdDto>? AuthorIds { get; set; }
}
