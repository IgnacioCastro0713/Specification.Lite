using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public sealed class AsSplitQueryEvaluator : IEvaluator
{
    public static AsSplitQueryEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Query<TEntity>(
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
