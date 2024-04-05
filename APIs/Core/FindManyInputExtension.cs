

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

    public static IQueryable<M> ApplyOrderBy<M>(this IQueryable<M> query, IEnumerable<OrderByInput>? orderBy) where M : class
    {
        if (orderBy == null)
        {
            return query;
        }

        foreach (var orderByInput in orderBy)
        {
            var propertyInfo = typeof(M).GetProperty(orderByInput.FieldName);
            if (propertyInfo == null)
            {
                continue;
            }
            query = orderByInput.SortOrder switch
            {
                SortOrder.Asc => query.OrderBy(x => propertyInfo.GetValue(x, null)),
                SortOrder.Des => query.OrderByDescending(x => propertyInfo.GetValue(x, null)),
                _ => query
            };
        }

        return query;
    }
}