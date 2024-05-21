using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class AuthorWhereInput
{
    [Range(1, long.MaxValue)]
    public long? Id { get; set; }

    [StringLength(250)]
    public string? Name { get; set; }

    // Missing the filter on relationship TodoItemIds
}
