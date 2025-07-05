using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class SpecificationAsNoTrackingEvaluator : IEvaluator
{
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
