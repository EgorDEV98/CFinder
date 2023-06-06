using System.Linq.Expressions;
using CFinder.Domain.WorkHistory;

namespace CFinder.Application.Utils.Filters;

public static class QueryableExtensions
{
    /// <summary>
    /// Выполняет фильтрацию OrderBy
    /// </summary>
    /// <param name="orderByExpression">x => x.StartDate предикат</param>
    /// <param name="orderBy">string (asc | desc)</param>
    /// <returns></returns>
    public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> query, Expression<Func<T, object>> orderByExpression, string orderBy)
    {
        switch (orderBy.ToLower())
        {
            case "asc":
                query = query.OrderBy(orderByExpression);
                break;
            case "desc":
                query = query.OrderByDescending(orderByExpression);
                break;
            default:
                // По умолчанию применяется сортировка по возрастанию
                query = query.OrderBy(orderByExpression);
                break;
        }

        return query;
    }
    
    /// <summary>
    /// Выполняет ранжирование от до
    /// </summary>
    /// <param name="query">Предикат</param>
    /// <param name="from">от (default 1)</param>
    /// <param name="to">до (default 50)</param>
    /// <returns></returns>
    public static IQueryable<WorkHistory>? ApplyRange(this IQueryable<WorkHistory>? query, int? from = 1, int? to = 50)
    {
        query = query.Where(x => x.Id >= from && x.Id <= to);
        return query;
    }
}