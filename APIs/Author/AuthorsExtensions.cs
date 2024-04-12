using MyService.APIs.Dtos;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Extensions;

public static class AuthorsExtensions
{
    public static AuthorDto ToDto(this Author model)
    {
        return new AuthorDto
        {
            Id = model.Id,
            Name = model.Name,
            TodoItemIds = model.TodoItems.Select(x => new TodoItemIdDto { Id = x.Id }),
        };
    }

    public static Author ToModel(this AuthorUpdateInput updateDto, AuthorIdDto idDto)
    {
        var author = new Author { Id = idDto.Id, Name = updateDto.Name, };

        return author;
    }
}
