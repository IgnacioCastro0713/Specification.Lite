using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> IgnoreQueryFilters<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder) where TEntity : class
    {
        IgnoreQueryFilters<TEntity>(builder);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> IgnoreQueryFilters<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.IgnoreQueryFilters = true;

        return builder;
    }
}
