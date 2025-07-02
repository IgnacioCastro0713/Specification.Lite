using System.Linq.Expressions;
using Specification.Lite.Builders;
using Specification.Lite.Expressions;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
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
}
