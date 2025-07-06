using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> IgnoreAutoIncludes<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder) where TEntity : class
    {
        IgnoreAutoIncludes<TEntity>(builder);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> IgnoreAutoIncludes<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.IgnoreAutoIncludes = true;

        return builder;
    }
}
