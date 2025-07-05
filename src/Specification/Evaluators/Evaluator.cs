namespace Specification.Lite.Evaluators;

public class Evaluator
{
    public static Evaluator Instance { get; } = new();

    private readonly List<IEvaluator> _evaluators =
    [
        WhereEvaluator.Instance,
        IncludesEvaluator.Instance,
        OrderEvaluator.Instance,
        PagingEvaluator.Instance,
        AsNoTrackingEvaluator.Instance,
        AsTrackingEvaluator.Instance,
        IgnoreQueryFiltersEvaluator.Instance,
        SplitQueryEvaluator.Instance
    ];

    public IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(specification);

        return _evaluators.Aggregate(query, (current, evaluator) => evaluator.Evaluate(current, specification));
    }
}
