using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Exceptions;
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
    public bool IsAsTracking { get; internal set; }
    public bool IsAsNoTracking { get; internal set; }
    public bool IsAsSplitQuery { get; internal set; }
}

public abstract class Specification<TEntity, TResult> : Specification<TEntity>, ISpecification<TEntity, TResult>
    where TEntity : class
{
    public new ISpecificationBuilder<TEntity, TResult> Query => new SpecificationBuilder<TEntity, TResult>(this);

    public Expression<Func<TEntity, TResult>>? Selector { get; protected set; }
    public Expression<Func<TEntity, IEnumerable<TResult>>>? SelectManySelector { get; protected set; }

    protected void Select(Expression<Func<TEntity, TResult>> selector)
    {
        if (SelectManySelector is not null)
        {
            throw new ConcurrentSelectorsException();
        }

        Selector = selector;
    }

    protected void SelectMany(Expression<Func<TEntity, IEnumerable<TResult>>> selector)
    {
        if (Selector is not null)
        {
            throw new ConcurrentSelectorsException();
        }

        SelectManySelector = selector;
    }
}
