using Specification.Lite.Evaluators;

namespace Specification.Lite;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> WithSpecification<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return query
            .Where(specification)
            .Include(specification)
            .Order(specification)
            .Paging(specification)
            .Tracking(specification)
            .SplitQuery(specification);
    }

    public static IQueryable<TResult> WithSpecification<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification)
        where TEntity : class
    {
        return query
            .WithSpecification<TEntity>(specification)
            .Selectors(specification);
    }
}
