using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Builders;


public class OrderingBuilder<TEntity>
{
    private readonly OrderExpression<TEntity> _orderExpression;

    internal OrderingBuilder(OrderExpression<TEntity> orderExpression) => _orderExpression = orderExpression;


    public OrderingBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> thenByExpression)
    {
        _orderExpression.ThenBy(thenByExpression);
        return new OrderingBuilder<TEntity>(_orderExpression);
    }

    public OrderingBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> thenByDescendingExpression)
    {
        _orderExpression.ThenByDescending(thenByDescendingExpression);
        return new OrderingBuilder<TEntity>(_orderExpression);
    }
}
