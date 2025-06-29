using System.Linq.Expressions;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public List<Expression<Func<TEntity, bool>>> CriteriaExpressions { get; } = [];
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
    public List<string> IncludeStrings { get; } = [];
    public List<OrderExpression<TEntity>> OrderByExpressions { get; } = [];
    public int Take { get; protected set; }
    public int Skip { get; protected set; }
    public bool IsAsTracking { get; private set; }
    public bool IsAsNoTracking { get; private set; }
    public bool IsAsSplitQuery { get; private set; }
    public bool IsDistinct { get; private set; }
    public Expression<Func<TEntity, object>>? DistinctBySelector { get; private set; }

    protected void AddWhere(Expression<Func<TEntity, bool>> criteriaExpression) => CriteriaExpressions.Add(criteriaExpression);

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => Includes.Add(includeExpression);

    protected void AddInclude(string includeString) => IncludeStrings.Add(includeString);

    protected void ApplyDistinct()
    {
        if (DistinctBySelector is not null)
        {
            throw new ConcurrentDistinctException();
        }

        IsDistinct = true;
    }

    protected void ApplyDistinctBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
    {
        if (IsDistinct)
        {
            throw new ConcurrentDistinctException();
        }

        DistinctBySelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(keySelector.Body, typeof(object)),
            keySelector.Parameters);
    }

    protected void AddOrderBy<TKey>(Expression<Func<TEntity, TKey>> orderByExpression)
    {
        var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(orderByExpression.Body, typeof(object)),
            orderByExpression.Parameters);
        OrderByExpressions.Add(new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.OrderBy));
    }

    protected void AddOrderByDescending<TKey>(Expression<Func<TEntity, TKey>> orderByDescendingExpression)
    {
        var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(orderByDescendingExpression.Body, typeof(object)),
            orderByDescendingExpression.Parameters);
        OrderByExpressions.Add(new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.OrderByDescending));
    }

    protected void AddThenBy<TKey>(Expression<Func<TEntity, TKey>> thenByExpression)
    {
        var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(thenByExpression.Body, typeof(object)),
            thenByExpression.Parameters);
        OrderByExpressions.Add(new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.ThenBy));
    }

    protected void AddThenByDescending<TKey>(Expression<Func<TEntity, TKey>> thenByDescendingExpression)
    {
        var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(thenByDescendingExpression.Body, typeof(object)),
            thenByDescendingExpression.Parameters);
        OrderByExpressions.Add(new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.ThenByDescending));
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

    protected void ApplySelect(Expression<Func<TEntity, TResult>> selector)
    {
        if (SelectManySelector is not null)
        {
            throw new ConcurrentSelectorsException();
        }

        Selector = selector;
    }

    protected void ApplySelectMany(Expression<Func<TEntity, IEnumerable<TResult>>> selector)
    {
        if (Selector is not null)
        {
            throw new ConcurrentSelectorsException();
        }

        SelectManySelector = selector;
    }
}
