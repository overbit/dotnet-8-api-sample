
namespace MyService.APIs;

public abstract class FindManyInput<M, W> : PaginationInput, IFindManyInput<M, W> where W : class
{
    public W? Where { get; set; }

    public IEnumerable<OrderByInput>? OrderBy { get; set; }

}