namespace Specification.Lite.Evaluators;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> SpecificationQuery<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IQueryable<TEntity> baseQuery = query
            .ApplyCriteria(specification)
            .ApplyIncludes(specification)
            .ApplyOrderBy(specification)
            .ApplyDistinctness(specification)
            .ApplyPaging(specification)
            .ApplyTracking(specification)
            .ApplySplitQuery(specification);


        return baseQuery;
    }

    public static IQueryable<TResult> SpecificationQuery<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification)
        where TEntity : class
    {
        IQueryable<TEntity> baseQuery = query.SpecificationQuery<TEntity>(specification);

        return baseQuery.ApplySelectors(specification);
    }
}
