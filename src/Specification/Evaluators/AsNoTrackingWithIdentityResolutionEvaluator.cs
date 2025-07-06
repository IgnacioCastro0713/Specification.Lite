using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public sealed class AsNoTrackingWithIdentityResolutionEvaluator : IEvaluator
{
    public static AsNoTrackingWithIdentityResolutionEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsNoTrackingWithIdentityResolution)
        {
            query = query.AsNoTrackingWithIdentityResolution();
        }
        return query;
    }
}
