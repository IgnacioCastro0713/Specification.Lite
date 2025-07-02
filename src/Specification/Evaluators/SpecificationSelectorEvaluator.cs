using Specification.Lite.Exceptions;

namespace Specification.Lite.Evaluators;

public static class SpecificationSelectorEvaluator
{
    internal static IQueryable<TResult> ApplySelectors<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification) where TEntity : class
    {

        if (specification.Selector is not null)
        {
            return query.Select(specification.Selector);
        }

        if (specification.SelectManySelector is not null)
        {
            return query.SelectMany(specification.SelectManySelector);
        }

        throw new DuplicateSelectorsException();
    }
}
