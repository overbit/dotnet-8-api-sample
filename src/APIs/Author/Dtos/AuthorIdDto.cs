using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class AuthorIdDto
{
    [Range(1, long.MaxValue)]
    public long Id { get; set; }
}
