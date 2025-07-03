using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public static class SpecificationAsTrackingEvaluator
{
    internal static IQueryable<TEntity> AsTracking<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsTracking)
        {
            query = query.AsTracking();
        }

        return query;
    }
}
