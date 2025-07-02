using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public static class SpecificationSplitQueryEvaluator
{
    internal static IQueryable<TEntity> SplitQuery<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.IsAsSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query;
    }
}
