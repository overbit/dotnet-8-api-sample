using MyService.APIs.Dtos;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Extensions;

public static class TodoItemsExtensions
{
    public static TodoItemIdDto ToIdDto(this TodoItem model)
    {
        return new TodoItemIdDto { Id = model.Id };
    }

    public static TodoItemDto ToDto(this TodoItem model)
    {
        return new TodoItemDto
        {
            Id = model.Id,
            Name = model.Name,
            IsComplete = model.IsComplete,
            AuthorIds = model.Authors.Select(x => new AuthorIdDto { Id = x.Id }).ToList()
        };
    }
}
