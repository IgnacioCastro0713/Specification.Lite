using System.Linq.Expressions;
using Specification.Lite.Common;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public static class SpecificationOrderByEvaluator
{
    public static IQueryable<TEntity> ApplyOrderBy<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IOrderedQueryable<TEntity>? orderedQuery = null;
        int countPrimaryOrder = 0;

        foreach (OrderExpression<TEntity> orderExpression in specification.OrderByExpressions)
        {
            if (countPrimaryOrder == 1)
            {
                throw new DuplicateOrderChainException();
            }

            if (orderExpression.OrderType == OrderType.OrderBy)
            {
                orderedQuery = query.OrderBy(orderExpression.Expression);
                countPrimaryOrder++;
            }

            if (orderExpression.OrderType == OrderType.OrderByDescending)
            {
                orderedQuery = query.OrderByDescending(orderExpression.Expression);
                countPrimaryOrder++;
            }


            foreach ((Expression<Func<TEntity, object>> thenExpression, OrderType thenOrderType) in orderExpression.ThenOrders)
            {
                if (thenOrderType == OrderType.ThenBy)
                {
                    orderedQuery = orderedQuery!.ThenBy(thenExpression);
                }

                if (thenOrderType == OrderType.ThenByDescending)
                {
                    orderedQuery = orderedQuery!.ThenByDescending(thenExpression);
                }
            }
        }

        return orderedQuery ?? query;
    }
}
