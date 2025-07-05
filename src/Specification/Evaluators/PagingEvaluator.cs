namespace Specification.Lite.Evaluators;

public class PagingEvaluator : IEvaluator
{
    public static PagingEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.Skip > 0)
        {
            query = query.Skip(specification.Skip);
        }

        if (specification.Take >= 0)
        {
            query = query.Take(specification.Take);
        }

        return query;
    }
}
