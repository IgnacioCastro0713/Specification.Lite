using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public class SpecificationSplitQueryEvaluator : IEvaluator
{
    public IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query;
    }
}
