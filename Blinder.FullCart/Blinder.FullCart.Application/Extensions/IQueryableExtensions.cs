using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blinder.FullCart.Application.Extensions;

public static class IQueryableExtensions
{
    /// <summary>
    /// this is useful for pagination , if you are working with ef please call Orderby Function after calling this function 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="PageNumber"></param>
    /// <param name="PageSize"></param>
    /// <returns></returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> source, int PageNumber = 1, int PageSize = 10)
    {
        var q = source
             .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        return q;
    }

    public static IQueryable<T> HasOrderBy<T>(this IQueryable<T> source, string ColumnName, SortDirections dir)
    {
        ColumnName = ColumnName.ToPascalCase();
        source = dir == SortDirections.Asc ? source.OrderBy(ColumnName) : source.OrderByDescending(ColumnName);
        return source;
    }

    static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDescending(ToLambda<T>(propertyName));
    }

    static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}