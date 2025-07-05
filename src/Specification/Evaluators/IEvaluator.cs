namespace Specification.Lite.Evaluators;

public interface IEvaluator
{
    IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class;
}
