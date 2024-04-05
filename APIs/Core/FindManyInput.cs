using MyService.APIs.Core.Attributes;

namespace MyService.APIs;

public abstract class FindManyInput<M, W> : PaginationInput, IFindManyInput<M, W> where W : class
{
    public W? Where { get; set; }

    /// <summary>
    /// Sort by a list of properties in the format of "property:asc" or "property:desc"
    /// </summary>
    /// <example>["Id:asc", "Id:desc"]</example>
    [RegularExpressionEnumerable(@"^([a-zA-Z0-9]+):(asc|desc)")]
    public IEnumerable<string>? SortBy { get; set; }
}