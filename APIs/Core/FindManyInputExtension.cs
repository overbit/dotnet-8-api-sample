

using System.Linq.Dynamic.Core;

namespace MyService.APIs;

public static class FindManyInputExtension
{
    public static IQueryable<M> ApplyTake<M>(this IQueryable<M> queryable, int? input) where M : class
    {
        if (input.HasValue)
        {
            queryable = queryable.Take(input.Value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplySkip<M>(this IQueryable<M> queryable, int? input) where M : class
    {
        if (input.HasValue)
        {
            queryable = queryable.Skip(input.Value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplyOrderBy<M>(this IQueryable<M> query, IEnumerable<string>? sortBy) where M : class
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
            var sortDirection = inputParts.Last() == "desc" ? SortDirection.Desc : SortDirection.Asc;

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