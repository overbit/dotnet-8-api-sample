
namespace MyService.APIs;

public class OrderByInput
{
    public required SortOrder SortOrder { get; set; }
    public required string FieldName { get; set; }
}