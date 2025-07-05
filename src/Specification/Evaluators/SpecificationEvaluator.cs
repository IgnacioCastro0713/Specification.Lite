namespace Specification.Lite.Evaluators;

public class SpecificationEvaluator
{
    public static SpecificationEvaluator Instance { get; } = new();

    private readonly List<IEvaluator> _evaluators =
    [
        new SpecificationWhereEvaluator(),
        new SpecificationIncludesEvaluator(),
        new SpecificationOrderEvaluator(),
        new SpecificationPagingEvaluator(),
        new SpecificationAsNoTrackingEvaluator(),
        new SpecificationAsTrackingEvaluator(),
        new SpecificationIgnoreQueryFiltersEvaluator(),
        new SpecificationSplitQueryEvaluator(),
    ];

    public IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(specification);

        return _evaluators
            .Aggregate(query, (current, evaluator) => evaluator.Evaluate(current, specification));
    }
}
