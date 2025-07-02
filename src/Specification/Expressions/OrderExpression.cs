using System.Linq.Expressions;
using Specification.Lite.Common;

namespace Specification.Lite.Expressions;

public class OrderExpression<TEntity>(
    Expression<Func<TEntity, object>> expression,
    OrderType orderType)
{
    internal Expression<Func<TEntity, object>> Expression { get; } = expression;
    internal OrderType OrderType { get; } = orderType;
    internal List<(Expression<Func<TEntity, object>> expression, OrderType orderType)> ThenOrders { get; } = [];


    public OrderExpression<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> thenByExpression)
    {
        var convertedSelector = System.Linq.Expressions.Expression.Lambda<Func<TEntity, object>>(
            System.Linq.Expressions.Expression.Convert(thenByExpression.Body, typeof(object)),
            thenByExpression.Parameters);
        ThenOrders.Add((convertedSelector, OrderType.ThenBy));
        return this;
    }

    public OrderExpression<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> thenByDescendingExpression)
    {
        var convertedSelector = System.Linq.Expressions.Expression.Lambda<Func<TEntity, object>>(
            System.Linq.Expressions.Expression.Convert(thenByDescendingExpression.Body, typeof(object)),
            thenByDescendingExpression.Parameters);
        ThenOrders.Add((convertedSelector, OrderType.ThenByDescending));
        return this;
    }
}
