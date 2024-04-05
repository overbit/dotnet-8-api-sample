using System.Linq.Expressions;
using MyService.APIs.Dtos;
using MyService.Infrastructure.Models;

namespace MyService.APIs.Extensions;
public static class AuthorWhereInputExtensions
{
    public static Expression<Func<Author, bool>> ToWherePredicate(this AuthorFindMany input)
    {
        var where = input.Where;

        return (model) =>
            where == null ||
            (where.Id == null || model.Id == where.Id) &&
            (where.Name == null || model.Name == where.Name);
    }
}