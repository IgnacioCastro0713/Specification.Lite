using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Common;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public static class SpecificationBuilderExtensions
{
    #region Where

    public static ISpecificationBuilder<TEntity> Where<TEntity>(
        this ISpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, bool>> criteriaExpression)
    {
        builder.Specification.WhereExpressions.Add(criteriaExpression);

        return builder;
    }

    #endregion

    #region Include

    public static IIncludeBuilder<TEntity, TProperty> Include<TEntity, TResult, TProperty>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, TProperty>> includeExpression) where TEntity : class
    {
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(includeExpression));
        var includeBuilder = new IncludeBuilder<TEntity, TResult, TProperty>(builder.Specification);

        return includeBuilder;
    }

    public static IIncludeBuilder<TEntity, TProperty> Include<TEntity, TProperty>(
        this ISpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, TProperty>> includeExpression) where TEntity : class
    {
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(includeExpression));
        var includeBuilder = new IncludeBuilder<TEntity, TProperty>(builder.Specification);

        return includeBuilder;
    }


    public static IIncludeBuilder<TEntity, TResult, TProperty> ThenInclude<TEntity, TResult, TPreviousProperty, TProperty>(
        this IIncludeBuilder<TEntity, TResult, TProperty> builder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where TEntity : class
    {
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(thenIncludeExpression, typeof(TPreviousProperty)));

        var includeBuilder = new IncludeBuilder<TEntity, TResult, TProperty>(builder.Specification);

        return includeBuilder;
    }

    public static IIncludeBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
        this IIncludeBuilder<TEntity, TPreviousProperty> builder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where TEntity : class
    {
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(thenIncludeExpression, typeof(TPreviousProperty)));

        var includeBuilder = new IncludeBuilder<TEntity, TProperty>(builder.Specification);

        return includeBuilder;
    }

    public static IIncludeBuilder<TEntity, TResult, TProperty> ThenInclude<TEntity, TResult, TPreviousProperty, TProperty>(
        this IIncludeBuilder<TEntity, TResult, IEnumerable<TPreviousProperty>> builder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where TEntity : class
    {
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(thenIncludeExpression, typeof(IEnumerable<TPreviousProperty>)));

        var includeBuilder = new IncludeBuilder<TEntity, TResult, TProperty>(builder.Specification);

        return includeBuilder;
    }

    public static IIncludeBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
        this IIncludeBuilder<TEntity, IEnumerable<TPreviousProperty>> builder,
        Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression) where TEntity : class
    {
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(thenIncludeExpression, typeof(IEnumerable<TPreviousProperty>)));

        var includeBuilder = new IncludeBuilder<TEntity, TProperty>(builder.Specification);

        return includeBuilder;
    }

    #endregion

    #region Order

    public static IOrderedSpecificationBuilder<TEntity, TResult> OrderBy<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, object?>> keySelector) where TEntity : class
    {
        builder.OrderBy<TEntity>(keySelector);
        return (SpecificationBuilder<TEntity, TResult>)builder;
    }

    public static IOrderedSpecificationBuilder<TEntity> OrderBy<TEntity>(
        this ISpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> keySelector)
    {
        builder.Specification.OrderExpressions.Add(new OrderExpression<TEntity>(keySelector, OrderType.OrderBy));

        return (SpecificationBuilder<TEntity>)builder;
    }

    public static IOrderedSpecificationBuilder<TEntity, TResult> OrderByDescending<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, object?>> keySelector) where TEntity : class
    {
        builder.OrderByDescending<TEntity>(keySelector);
        return (SpecificationBuilder<TEntity, TResult>)builder;
    }

    public static IOrderedSpecificationBuilder<TEntity> OrderByDescending<TEntity>(
        this ISpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> keySelector)
    {
        builder.Specification.OrderExpressions.Add(new OrderExpression<TEntity>(keySelector, OrderType.OrderByDescending));

        return (SpecificationBuilder<TEntity>)builder;
    }

    public static IOrderedSpecificationBuilder<TEntity, TResult> ThenBy<TEntity, TResult>(
        this IOrderedSpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, object?>> keySelector) where TEntity : class
    {
        builder.ThenBy<TEntity>(keySelector);
        return builder;
    }

    public static IOrderedSpecificationBuilder<TEntity> ThenBy<TEntity>(
        this IOrderedSpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> keySelector)
    {
        builder.Specification.OrderExpressions.Add(new OrderExpression<TEntity>(keySelector, OrderType.ThenBy));

        return builder;
    }

    public static IOrderedSpecificationBuilder<TEntity, TResult> ThenByDescending<TEntity, TResult>(
        this IOrderedSpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, object?>> keySelector) where TEntity : class
    {
        builder.ThenByDescending<TEntity>(keySelector);
        return builder;
    }

    public static IOrderedSpecificationBuilder<TEntity> ThenByDescending<TEntity>(
        this IOrderedSpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> keySelector)
    {
        builder.Specification.OrderExpressions.Add(new OrderExpression<TEntity>(keySelector, OrderType.ThenByDescending));

        return builder;
    }

    #endregion

    #region Paging

    public static ISpecificationBuilder<TEntity> Take<TEntity>(this ISpecificationBuilder<TEntity> builder, int value)
    {
        builder.Specification.Take = value;

        return builder;
    }

    public static ISpecificationBuilder<TEntity> Skip<TEntity>(this ISpecificationBuilder<TEntity> builder, int value)
    {
        builder.Specification.Skip = value;

        return builder;
    }

    #endregion

    #region Tracking

    public static ISpecificationBuilder<TEntity> AsTracking<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        if (builder.Specification.IsAsNoTracking)
        {
            throw new ConcurrentTrackingException();
        }

        builder.Specification.IsAsTracking = true;

        return builder;
    }

    public static ISpecificationBuilder<TEntity> AsNoTracking<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        if (builder.Specification.IsAsTracking)
        {
            throw new ConcurrentTrackingException();
        }

        builder.Specification.IsAsNoTracking = true;

        return builder;
    }

    #endregion

    #region SplitQuery

    public static ISpecificationBuilder<TEntity> SplitQuery<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.IsAsSplitQuery = true;

        return builder;
    }

    #endregion
}
