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
        int chainCount = 0;
        foreach (OrderExpression<TEntity> orderExpression in specification.OrderByExpressions)
        {
            switch (orderExpression)
            {
                case { OrderType: OrderTypeEnum.OrderBy }:
                    {
                        chainCount++;
                        if (chainCount == 2)
                        {
                            throw new DuplicateOrderChainException();
                        }

                        orderedQuery = query.OrderBy(orderExpression.KeySelector);
                        break;
                    }
                case { OrderType: OrderTypeEnum.OrderByDescending }:
                    {
                        chainCount++;
                        if (chainCount == 2)
                        {
                            throw new DuplicateOrderChainException();
                        }

                        orderedQuery = query.OrderByDescending(orderExpression.KeySelector);
                        break;
                    }
                case { OrderType: OrderTypeEnum.ThenBy }:
                    orderedQuery = orderedQuery!.ThenBy(orderExpression.KeySelector);
                    break;
                case { OrderType: OrderTypeEnum.ThenByDescending }:
                    orderedQuery = orderedQuery!.ThenByDescending(orderExpression.KeySelector);
                    break;
            }
        }

        if (orderedQuery is not null)
        {
            query = orderedQuery;
        }

        return query;
    }
}
