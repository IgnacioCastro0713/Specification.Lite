using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class SpecificationIgnoreQueryFiltersEvaluator : IEvaluator
{
    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.IgnoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }
}
