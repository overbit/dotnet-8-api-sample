using System.Linq.Dynamic.Core;

namespace MyService.APIs.Common;

public static class FindManyInputExtension
{
    public static IQueryable<M> ApplyWhere<M, W>(this IQueryable<M> queryable, W? where)
        where M : class
    {
        if (where == null)
        {
            return queryable;
        }

        var properties = typeof(W).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(where, null);
            if (value == null)
            {
                continue;
            }

            queryable = queryable.Where($"{property.Name} == @0", value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplyTake<M>(this IQueryable<M> queryable, int? input)
        where M : class
    {
        if (input.HasValue)
        {
            queryable = queryable.Take(input.Value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplySkip<M>(this IQueryable<M> queryable, int? input)
        where M : class
    {
        if (input.HasValue)
        {
            queryable = queryable.Skip(input.Value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplyOrderBy<M>(
        this IQueryable<M> query,
        IEnumerable<string>? sortBy
    )
        where M : class
    {
        if (sortBy == null)
        {
            return query;
        }

        string[] orderByStatements = [];
        foreach (var sortByInput in sortBy)
        {
            var inputParts = sortByInput.Split(':');
            var fieldName = inputParts.First();
            var sortDirection =
                inputParts.Last() == "desc" ? SortDirection.Desc : SortDirection.Asc;

            var propertyInfo = typeof(M).GetProperty(fieldName);
            if (propertyInfo == null)
            {
                continue;
            }

            switch (sortDirection)
            {
                case SortDirection.Asc:
                    orderByStatements = orderByStatements.Append(fieldName).ToArray();
                    break;
                case SortDirection.Desc:
                    orderByStatements = orderByStatements.Append($"{fieldName} desc").ToArray();
                    break;
                default:
                    break;
            }
        }

        return query.OrderBy(String.Join(", ", orderByStatements));
    }
}
