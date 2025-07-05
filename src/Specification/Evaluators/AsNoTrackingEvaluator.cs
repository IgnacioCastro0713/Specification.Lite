using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class AsNoTrackingEvaluator : IEvaluator
{
    public static AsNoTrackingEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsNoTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
}
