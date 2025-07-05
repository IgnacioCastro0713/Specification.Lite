namespace Specification.Lite.Evaluators;

public class SpecificationWhereEvaluator : IEvaluator
{
    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return specification.WhereExpressions.Count == 0
            ? query
            : specification
                .WhereExpressions
                .Aggregate(query, (current, whereExpression) => current.Where(whereExpression));
    }
}
