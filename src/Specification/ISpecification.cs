using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public interface ISpecification<TEntity>
{
    List<Expression<Func<TEntity, bool>>> WhereExpressions { get; }
    List<IncludeExpression> IncludeExpressions { get; }
    List<OrderExpression<TEntity>> OrderExpressions { get; }
    int Take { get; }
    int Skip { get; }
    bool AsTracking { get; }
    bool AsNoTracking { get; }
    bool AsNoTrackingWithIdentityResolution { get; }
    bool AsSplitQuery { get; }
    bool IgnoreQueryFilters { get; }
    bool IgnoreAutoIncludes { get; }
}

public interface ISpecification<TEntity, TResult> : ISpecification<TEntity>
{
    Expression<Func<TEntity, TResult>>? Selector { get; }
    Expression<Func<TEntity, IEnumerable<TResult>>>? ManySelector { get; }
}
