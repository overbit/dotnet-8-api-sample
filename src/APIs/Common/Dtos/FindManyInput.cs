using MyService.APIs.Common.Attributes;

namespace MyService.APIs.Common;

public abstract class FindManyInput<M, W> : PaginationInput, IFindManyInput<M, W>
    where W : class
{
    public W? Where { get; set; }

    /// <summary>
    /// Sort by a list of properties in the format of "property:asc" or "property:desc"
    /// </summary>
    [RegularExpressionEnumerable(@"^([a-zA-Z0-9]+):(asc|desc)")]
    public IEnumerable<string>? SortBy { get; set; }
}
