using MyService.APIs.Dtos;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Extensions;

public static class TodoItemsExtensions
{
    public static TodoItemDto ToDto(this TodoItem model)
    {
        return new TodoItemDto
        {
            Id = model.Id,
            Name = model.Name,
            IsComplete = model.IsComplete,
            WorkspaceId = new WorkspaceIdDto { Id = model.WorkspaceId },
            AuthorIds = model.Authors.Select(x => new AuthorIdDto { Id = x.Id }).ToList()
        };
    }

    public static TodoItem ToModel(this TodoItemUpdateInput updateDto, TodoItemIdDto idDto)
    {
        var todoItem = new TodoItem { Id = idDto.Id, Name = updateDto.Name, };

        if (updateDto.IsComplete != null)
        {
            todoItem.IsComplete = updateDto.IsComplete.Value;
        }

        if (updateDto.workspaceId != null)
        {
            todoItem.WorkspaceId = updateDto.workspaceId.Value;
        }

        return todoItem;
    }
}
