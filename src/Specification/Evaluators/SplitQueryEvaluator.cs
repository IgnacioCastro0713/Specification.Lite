using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class SplitQueryEvaluator : IEvaluator
{
    public static SplitQueryEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query;
    }
}
