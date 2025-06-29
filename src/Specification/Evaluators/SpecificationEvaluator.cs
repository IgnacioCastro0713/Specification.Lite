namespace Specification.Lite.Evaluators;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> SpecifyQuery<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return query
            .ApplyCriteria(specification)
            .ApplyIncludes(specification)
            .ApplyOrderBy(specification)
            .ApplyDistinctness(specification)
            .ApplyPaging(specification)
            .ApplyTracking(specification)
            .ApplySplitQuery(specification);
    }

    public static IQueryable<TResult> SpecifyQuery<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification)
        where TEntity : class
    {
        IQueryable<TEntity> baseQuery = query.SpecifyQuery<TEntity>(specification);

        return baseQuery.ApplySelectors(specification);
    }
}
