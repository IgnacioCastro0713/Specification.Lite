using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> SplitQuery<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder) where TEntity : class
    {
        SplitQuery<TEntity>(builder);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> SplitQuery<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.IsAsSplitQuery = true;

        return builder;
    }
}
