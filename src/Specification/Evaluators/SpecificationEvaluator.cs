using Specification.Lite.Evaluators;

namespace Specification.Lite;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> WithSpecification<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IQueryable<TEntity> baseQuery = query
            .ApplyWhere(specification)
            .ApplyIncludes(specification)
            .ApplyOrderBy(specification)
            .ApplyPaging(specification)
            .ApplyTracking(specification)
            .ApplySplitQuery(specification);

        return baseQuery;
    }

    public static IQueryable<TResult> WithSpecification<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification)
        where TEntity : class
    {
        IQueryable<TEntity> baseQuery = query.WithSpecification<TEntity>(specification);

        return baseQuery.ApplySelectors(specification);
    }
}
