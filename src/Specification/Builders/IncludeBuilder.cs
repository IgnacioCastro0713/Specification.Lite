namespace Specification.Lite.Builders;
public interface IIncludeBuilder<TEntity, TResult, out TProperty>
    : ISpecificationBuilder<TEntity, TResult>, IIncludeBuilder<TEntity, TProperty>
    where TEntity : class;

public interface IIncludeBuilder<TEntity, out TProperty>
    : ISpecificationBuilder<TEntity> where TEntity : class;

public class IncludeBuilder<TEntity, TResult, TProperty>(Specification<TEntity, TResult> specification)
    : SpecificationBuilder<TEntity, TResult>(specification), IIncludeBuilder<TEntity, TResult, TProperty>
    where TEntity : class;

public class IncludeBuilder<TEntity, TProperty>(Specification<TEntity> specification)
    : SpecificationBuilder<TEntity>(specification), IIncludeBuilder<TEntity, TProperty>
    where TEntity : class;
