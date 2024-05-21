namespace MyService.APIs.Common;

public interface IFindManyInput<M, W>
{
    public W? Where { get; set; }

    public IEnumerable<string>? SortBy { get; set; }

    public int? Skip { get; set; }

    public int? Take { get; set; }
}
