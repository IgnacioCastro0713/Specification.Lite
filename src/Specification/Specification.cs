using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Common;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public List<Expression<Func<TEntity, bool>>> CriteriaExpressions { get; } = [];
    public List<IncludeExpression<TEntity>> IncludeExpressions { get; } = [];
    public List<OrderExpression<TEntity>> OrderByExpressions { get; } = [];
    public int Take { get; protected set; } = -1;
    public int Skip { get; protected set; } = -1;
    public bool IsAsTracking { get; private set; }
    public bool IsAsNoTracking { get; private set; }
    public bool IsAsSplitQuery { get; private set; }
    public bool IsDistinct { get; private set; }
    public Expression<Func<TEntity, object>>? DistinctBySelector { get; private set; }

    protected void Where(Expression<Func<TEntity, bool>> criteriaExpression) => CriteriaExpressions.Add(criteriaExpression);

    protected IncludeBuilder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> includeExpression)
    {
        var includePath = new IncludeExpression<TEntity>(includeExpression, false);
        IncludeExpressions.Add(includePath);
        return new IncludeBuilder<TEntity, TProperty>(includePath);
    }

    protected IncludeBuilder<TEntity, TProperty> Include<TProperty>(
        Expression<Func<TEntity, IEnumerable<TProperty>>> includeExpression)
    {
        var includePath = new IncludeExpression<TEntity>(includeExpression, true);
        IncludeExpressions.Add(includePath);
        return new IncludeBuilder<TEntity, TProperty>(includePath);
    }

    protected OrderingBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> orderByExpression)
    {
        var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(orderByExpression.Body, typeof(object)),
            orderByExpression.Parameters);
        var ordering = new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.OrderBy);
        OrderByExpressions.Add(ordering);
        return new OrderingBuilder<TEntity>(ordering);
    }

    protected OrderingBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> orderByDescendingExpression)
    {
        var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(orderByDescendingExpression.Body, typeof(object)),
            orderByDescendingExpression.Parameters);
        var ordering = new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.OrderByDescending);
        OrderByExpressions.Add(ordering);
        return new OrderingBuilder<TEntity>(ordering);
    }

    protected void Distinct()
    {
        if (DistinctBySelector is not null)
        {
            throw new ConcurrentDistinctException();
        }

        IsDistinct = true;
    }

    protected void DistinctBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
    {
        if (IsDistinct)
        {
            throw new ConcurrentDistinctException();
        }

        DistinctBySelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(keySelector.Body, typeof(object)),
            keySelector.Parameters);
    }

    protected void AsTracking()
    {
        if (IsAsNoTracking)
        {
            throw new ConcurrentTrackingException();
        }

        IsAsTracking = true;
    }

    protected void AsNoTracking()
    {
        if (IsAsTracking)
        {
            throw new ConcurrentTrackingException();
        }

        IsAsNoTracking = true;
    }

    protected void SplitQuery() => IsAsSplitQuery = true;
}

public abstract class Specification<TEntity, TResult> : Specification<TEntity>, ISpecification<TEntity, TResult>
    where TEntity : class
{
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
