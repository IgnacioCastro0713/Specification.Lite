namespace Specification.Lite.Evaluators;

public static class SpecificationWhereEvaluator
{
    internal static IQueryable<TEntity> ApplyWhere<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return specification.WhereExpressions.Count == 0
            ? query
            : specification
                .WhereExpressions
                .Aggregate(query, (current, whereExpression) => current.Where(whereExpression));
    }
}
