using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public sealed class TagWithEvaluator : IEvaluator
{
    public static TagWithEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return specification.QueryTags.Aggregate(query, (current, queryTag) => current.TagWith(queryTag));
    }
}
