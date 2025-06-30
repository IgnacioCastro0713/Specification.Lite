using System.Linq.Expressions;

namespace Specification.Lite.Common;

public class OrderExpression<TEntity>(
    Expression<Func<TEntity, object>> keySelector,
    OrderTypeEnum orderType)
{
    public Expression<Func<TEntity, object>> KeySelector { get; } = keySelector;
    public OrderTypeEnum OrderType { get; } = orderType;
}

public class OrderingBuilder<TEntity>
{
    private readonly Specification<TEntity> _specification;

    internal OrderingBuilder(Specification<TEntity> specification) => _specification = specification;

    public OrderingBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> thenByExpression)
    {
        return _specification.ThenBy(thenByExpression);
    }

    public OrderingBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> thenByDescendingExpression)
    {
        return _specification.ThenByDescending(thenByDescendingExpression);
    }
}
