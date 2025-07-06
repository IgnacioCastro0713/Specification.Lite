namespace Specification.Lite.Evaluators;

public interface IEvaluator
{
    IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class;
}
