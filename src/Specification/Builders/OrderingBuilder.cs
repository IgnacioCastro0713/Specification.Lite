using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Builders;


public class OrderingBuilder<TEntity>(OrderExpression<TEntity> orderExpression)
{
    public OrderingBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> thenByExpression)
    {
        orderExpression.ThenBy(thenByExpression);
        return new OrderingBuilder<TEntity>(orderExpression);
    }

    public OrderingBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> thenByDescendingExpression)
    {
        orderExpression.ThenByDescending(thenByDescendingExpression);
        return new OrderingBuilder<TEntity>(orderExpression);
    }
}
