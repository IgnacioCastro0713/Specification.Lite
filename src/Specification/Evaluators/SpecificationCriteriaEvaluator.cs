using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public static class SpecificationCriteriaEvaluator
{
    public static IQueryable<TEntity> ApplyCriteria<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.CriteriaExpressions.Count == 0)
        {
            return query;
        }

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");
        var visitor = new ParameterRebinderExpression(parameter);
        var bodies = specification.CriteriaExpressions
            .Select(expr => visitor.Visit(expr.Body))
            .ToList();

        Expression combinedBody = bodies.Aggregate(Expression.AndAlso);
        var combinedCriteria = Expression.Lambda<Func<TEntity, bool>>(combinedBody, parameter);
        query = query.Where(combinedCriteria);

        return query;
    }
}