using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class TodoItemIdDto
{
    [Range(1, long.MaxValue)]
    public long Id { get; set; }
}
