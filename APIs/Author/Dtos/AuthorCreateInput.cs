using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class AuthorCreateInput
{
    [Range(1, long.MaxValue)]
    public long? Id { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    public ICollection<TodoItemIdDto>? TodoItemIds { get; set; }
}
