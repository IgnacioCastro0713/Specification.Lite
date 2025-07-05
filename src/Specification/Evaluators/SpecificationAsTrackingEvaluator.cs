using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class SpecificationAsTrackingEvaluator : IEvaluator
{
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
