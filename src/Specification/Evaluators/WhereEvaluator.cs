namespace Specification.Lite.Evaluators;

public sealed class WhereEvaluator : IEvaluator
{
    public static WhereEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return specification.WhereExpressions.Count == 0
            ? query
            : specification.WhereExpressions
                .Aggregate(query, (current, whereExpression) => current.Where(whereExpression));
    }
}
