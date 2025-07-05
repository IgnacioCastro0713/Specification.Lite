using Specification.Lite.Common;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public sealed class OrderEvaluator : IEvaluator
{
    public static OrderEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
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
