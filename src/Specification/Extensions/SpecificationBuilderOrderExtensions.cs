using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Common;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
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
}
