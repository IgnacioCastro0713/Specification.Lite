namespace Specification.Lite.Evaluators;

public static class SpecificationDistinctnessEvaluator
{
    public static IQueryable<TEntity> ApplyDistinctness<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }
        else if (specification.DistinctBySelector is not null)
        {
            query = query.DistinctBy(specification.DistinctBySelector);
        }

        return query;
    }
}
