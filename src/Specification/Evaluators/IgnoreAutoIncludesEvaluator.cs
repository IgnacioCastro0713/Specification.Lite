using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public sealed class IgnoreAutoIncludesEvaluator : IEvaluator
{
    public static IgnoreAutoIncludesEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.IgnoreAutoIncludes)
        {
            query = query.IgnoreAutoIncludes();
        }

        return query;
    }
}
