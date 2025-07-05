using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> AsNoTrackingWithIdentityResolution<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder) where TEntity : class
    {
        AsNoTrackingWithIdentityResolution<TEntity>(builder);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> AsNoTrackingWithIdentityResolution<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.AsNoTrackingWithIdentityResolution = true;
        builder.Specification.AsTracking = false;
        builder.Specification.AsNoTracking = false;

        return builder;
    }
}
