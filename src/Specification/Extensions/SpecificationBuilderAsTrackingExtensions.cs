using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> AsTracking<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder) where TEntity : class
    {
        AsTracking<TEntity>(builder);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> AsTracking<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.AsTracking = true;
        builder.Specification.AsNoTracking = false;
        builder.Specification.AsNoTrackingWithIdentityResolution = false;

        return builder;
    }
}
