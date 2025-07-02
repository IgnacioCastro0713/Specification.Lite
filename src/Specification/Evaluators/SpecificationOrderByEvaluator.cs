using Specification.Lite.Common;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public static class SpecificationOrderByEvaluator
{
    internal static IQueryable<TEntity> ApplyOrderBy<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IOrderedQueryable<TEntity>? orderedQuery = specification
            .OrderExpressions
            .Aggregate<OrderExpression<TEntity>, IOrderedQueryable<TEntity>?>(null, (current, item) => item.OrderType switch
                {
                    OrderType.OrderBy => query.OrderBy(item.KeySelector),
                    OrderType.OrderByDescending => query.OrderByDescending(item.KeySelector),
                    OrderType.ThenBy => current!.ThenBy(item.KeySelector),
                    OrderType.ThenByDescending => current!.ThenByDescending(item.KeySelector),
                    _ => current
                });

        if (orderedQuery is not null)
        {
            query = orderedQuery;
        }

        return query;
    }
}
