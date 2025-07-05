using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class AsTrackingEvaluator : IEvaluator
{
    public static AsTrackingEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsTracking)
        {
            query = query.AsTracking();
        }

        return query;
    }
}
