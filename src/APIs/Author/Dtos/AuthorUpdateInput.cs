using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

// Equal to AuthorCreateInput but with all properties optional
public class AuthorUpdateInput
{

    [StringLength(250)]
    public string? Name { get; set; }

    public ICollection<TodoItemIdDto>? TodoItemIds { get; set; }
}
