using System.Linq.Expressions;
using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static void Select<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, TResult>> selector) where TEntity : class
    {
        builder.Specification.Selector = selector;
    }

    public static void SelectMany<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        Expression<Func<TEntity, IEnumerable<TResult>>> selector) where TEntity : class
    {
        builder.Specification.ManySelector = selector;
    }
}
