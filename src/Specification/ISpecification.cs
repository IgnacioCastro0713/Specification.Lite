using System.Linq.Expressions;
using Specification.Lite.Common;

namespace Specification.Lite;

public interface ISpecification<TEntity>
{
    List<Expression<Func<TEntity, bool>>> CriteriaExpressions { get; }
    List<IncludeExpression<TEntity>> IncludePaths { get; }
    List<OrderExpression<TEntity>> OrderByExpressions { get; }
    int Take { get; }
    int Skip { get; }
    public bool IsAsTracking { get; }
    public bool IsAsNoTracking { get; }
    public bool IsAsSplitQuery { get; }
    bool IsDistinct { get; }
    Expression<Func<TEntity, object>>? DistinctBySelector { get; }
}

public interface ISpecification<TEntity, TResult> : ISpecification<TEntity>
{
    Expression<Func<TEntity, TResult>>? Selector { get; }
    Expression<Func<TEntity, IEnumerable<TResult>>>? SelectManySelector { get; }
}
