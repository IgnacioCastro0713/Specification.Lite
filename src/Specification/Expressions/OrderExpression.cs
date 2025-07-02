using System.Linq.Expressions;
using Specification.Lite.Common;

namespace Specification.Lite.Expressions;

public class OrderExpression<TEntity>(
    Expression<Func<TEntity, object?>> expression,
    OrderType orderType)
{
    internal Expression<Func<TEntity, object?>> KeySelector { get; } = expression;
    internal OrderType OrderType { get; } = orderType;
}
