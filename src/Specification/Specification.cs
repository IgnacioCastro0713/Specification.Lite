using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public ISpecificationBuilder<TEntity> Query => new SpecificationBuilder<TEntity>(this);
    public List<Expression<Func<TEntity, bool>>> WhereExpressions { get; internal set; } = [];
    public List<IncludeExpression> IncludeExpressions { get; internal set; } = [];
    public List<OrderExpression<TEntity>> OrderExpressions { get; internal set; } = [];
    public int Take { get; internal set; } = -1;
    public int Skip { get; internal set; } = -1;
    public bool AsTracking { get; internal set; } = false;
    public bool AsNoTracking { get; internal set; } = false;
    public bool AsSplitQuery { get; internal set; } = false;
    public bool IgnoreQueryFilters { get; internal set; } = false;
}

public abstract class Specification<TEntity, TResult> : Specification<TEntity>, ISpecification<TEntity, TResult>
    where TEntity : class
{
    public new ISpecificationBuilder<TEntity, TResult> Query => new SpecificationBuilder<TEntity, TResult>(this);
    public Expression<Func<TEntity, TResult>>? Selector { get; internal set; }
    public Expression<Func<TEntity, IEnumerable<TResult>>>? ManySelector { get; internal set; }
}
