using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public static class SpecificationIgnoreQueryFiltersEvaluator
{
    internal static IQueryable<TEntity> IgnoreQueryFilters<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.IgnoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }
}
