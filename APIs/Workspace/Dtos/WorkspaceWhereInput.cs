namespace MyService.APIs.Dtos;

public class WorkspaceWhereInput
{
    public long? Id { get; set; }
    public string? Name { get; set; }

    // Missing the filter on relationship TodoItemIds
}
