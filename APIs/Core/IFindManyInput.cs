
namespace MyService.APIs;

public interface IFindManyInput<M, W>
{
    public W? Where { get; set; }

    public IEnumerable<OrderByInput>? OrderBy { get; set; }

    public int? Skip { get; set; }

    public int? Take { get; set; }
}