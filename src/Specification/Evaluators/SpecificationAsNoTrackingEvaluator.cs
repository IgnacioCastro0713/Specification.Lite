using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public static class SpecificationAsNoTrackingEvaluator
{
    internal static IQueryable<TEntity> AsNoTracking<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsNoTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
}
