using System.Linq.Expressions;
using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> Where<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        Where<TEntity>(builder, predicate);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> Where<TEntity>(
        this ISpecificationBuilder<TEntity> builder,
        Expression<Func<TEntity, bool>> criteriaExpression)
    {
        builder.Specification.WhereExpressions.Add(criteriaExpression);

        return builder;
    }
}
