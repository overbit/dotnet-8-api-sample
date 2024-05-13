using System.ComponentModel.DataAnnotations;

namespace MyService.APIs.Dtos;

public class WorkspaceIdDto
{
    [Range(1, long.MaxValue)]
    public long Id { get; set; }
}
