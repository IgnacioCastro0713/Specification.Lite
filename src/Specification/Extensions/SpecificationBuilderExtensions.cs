using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public static class SpecificationBuilderExtensions
{

    public static ISpecificationBuilder<TEntity> Where<TEntity>(
        this ISpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, bool>> criteriaExpression)
    {
        builder.Specification.WhereExpressions.Add(criteriaExpression);

        return builder;
    }

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

    //public static OrderingBuilder<TEntity> OrderBy<TEntity, TKey>(
    //    this ISpecificationBuilder<TEntity> builder,
    //    Expression<Func<TEntity, TKey>> orderByExpression)
    //{
    //    var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
    //        Expression.Convert(orderByExpression.Body, typeof(object)),
    //        orderByExpression.Parameters);
    //    var ordering = new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.OrderBy);
    //    builder.Specification.OrderByExpressions.Add(ordering);
    //    return new OrderingBuilder<TEntity>(ordering);
    //}

    //public static OrderingBuilder<TEntity> OrderByDescending<TEntity, TKey>(
    //    this ISpecificationBuilder<TEntity> builder,
    //    Expression<Func<TEntity, TKey>> orderByDescendingExpression)
    //{
    //    var convertedSelector = Expression.Lambda<Func<TEntity, object>>(
    //        Expression.Convert(orderByDescendingExpression.Body, typeof(object)),
    //        orderByDescendingExpression.Parameters);
    //    var ordering = new OrderExpression<TEntity>(convertedSelector, OrderTypeEnum.OrderByDescending);
    //    builder.Specification.OrderByExpressions.Add(ordering);
    //    return new OrderingBuilder<TEntity>(ordering);
    //}


    public static ISpecificationBuilder<TEntity> Take<TEntity>(this ISpecificationBuilder<TEntity> builder, int value)
    {
        builder.Specification.Take = value;

        return builder;
    }

    public static ISpecificationBuilder<TEntity> Skep<TEntity>(this ISpecificationBuilder<TEntity> builder, int value)
    {
        builder.Specification.Skip = value;

        return builder;
    }

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

    public static ISpecificationBuilder<TEntity> SplitQuery<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.IsAsSplitQuery = true;

        return builder;
    }
}
