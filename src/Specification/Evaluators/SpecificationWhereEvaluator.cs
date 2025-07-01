using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public static class SpecificationWhereEvaluator
{
    public static IQueryable<TEntity> ApplyWhere<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.WhereExpressions.Count == 0)
        {
            return query;
        }

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");
        var visitor = new ParameterRebinder(parameter);
        var bodies = specification.WhereExpressions
            .Select(expr => visitor.Visit(expr.Body))
            .ToList();

        Expression combinedBody = bodies.Aggregate(Expression.AndAlso);
        var combinedCriteria = Expression.Lambda<Func<TEntity, bool>>(combinedBody, parameter);
        query = query.Where(combinedCriteria);

        return query;
    }
}
