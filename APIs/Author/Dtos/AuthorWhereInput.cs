namespace MyService.APIs.Dtos;

public class AuthorWhereInput
{
    public long? Id { get; set; }
    public string? Name { get; set; }

    // Missing the filter on relationship TodoItemIds
}
