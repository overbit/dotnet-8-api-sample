using MyService.Infrastructure.Models;

namespace MyService.APIs.Dtos.Extensions;
public static class TodoItemsExtensions
{
    public static TodoItemDto ToDto(this TodoItem model)
    {
        return new TodoItemDto
        {
            Id = model.Id,
            Name = model.Name,
            IsComplete = model.IsComplete,
            Authors = model.Authors.Select(x => x.ToDto()).ToList()
        };
    }
}