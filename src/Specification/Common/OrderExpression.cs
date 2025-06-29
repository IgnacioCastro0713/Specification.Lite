using System.Linq.Expressions;

namespace Specification.Lite.Common;

public enum OrderTypeEnum
{
    OrderBy = 1,
    OrderByDescending = 2,
    ThenBy = 3,
    ThenByDescending = 4
}

public class OrderExpression<TEntity>(
    Expression<Func<TEntity, object>> keySelector,
    OrderTypeEnum orderType)
{
    public Expression<Func<TEntity, object>> KeySelector { get; } = keySelector;
    public OrderTypeEnum OrderType { get; } = orderType;
}