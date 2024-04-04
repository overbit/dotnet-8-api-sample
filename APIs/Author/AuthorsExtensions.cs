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
            TodoItems = model.TodoItems.Select(x => x.ToDto()).ToList(),
        };
    }
}