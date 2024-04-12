using MyService.APIs.Dtos;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Extensions;

public static class WorkspacesExtensions
{
    public static WorkspaceDto ToDto(this Workspace model)
    {
        return new WorkspaceDto
        {
            Id = model.Id,
            Name = model.Name,
            TodoItemIds = model.TodoItems.Select(x => new TodoItemIdDto { Id = x.Id }),
        };
    }

    public static Workspace ToModel(this WorkspaceUpdateInput updateDto, WorkspaceIdDto idDto)
    {
        var author = new Workspace { Id = idDto.Id, Name = updateDto.Name, };

        return author;
    }
}
