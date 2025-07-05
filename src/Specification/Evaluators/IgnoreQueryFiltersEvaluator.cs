using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class IgnoreQueryFiltersEvaluator : IEvaluator
{
    public static IgnoreQueryFiltersEvaluator Instance { get; } = new();

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
