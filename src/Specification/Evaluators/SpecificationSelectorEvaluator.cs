using Specification.Lite.Exceptions;

namespace Specification.Lite.Evaluators;

public static class SpecificationSelectorEvaluator
{
    internal static IQueryable<TResult> Selectors<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification) where TEntity : class
    {
        if (specification.Selector is null && specification.ManySelector is null)
        {
            throw new SelectorNotFoundException();
        }

        return specification.Selector is not null
            ? query.Select(specification.Selector)
            : query.SelectMany(specification.ManySelector!);
    }
}
