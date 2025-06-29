using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public static class SpecificationTrackingEvaluator
{
    public static IQueryable<TEntity> ApplyTracking<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.IsAsNoTracking)
        {
            query = query.AsNoTracking();
        }
        else if (specification.IsAsTracking)
        {
            query = query.AsTracking();
        }

        return query;
    }
}
