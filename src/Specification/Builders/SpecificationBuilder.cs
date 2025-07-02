namespace Specification.Lite.Builders;

public interface ISpecificationBuilder<TEntity, TResult>
    : ISpecificationBuilder<TEntity> where TEntity : class
{
    new Specification<TEntity, TResult> Specification { get; }
}

public interface ISpecificationBuilder<TEntity>
{
    Specification<TEntity> Specification { get; }
}

public class SpecificationBuilder<TEntity, TResult>(Specification<TEntity, TResult> specification)
    : SpecificationBuilder<TEntity>(specification), ISpecificationBuilder<TEntity, TResult> where TEntity : class
{
    public new Specification<TEntity, TResult> Specification { get; } = specification;
}

public class SpecificationBuilder<TEntity>(Specification<TEntity> specification) : ISpecificationBuilder<TEntity>
{
    public Specification<TEntity> Specification { get; } = specification;
}
