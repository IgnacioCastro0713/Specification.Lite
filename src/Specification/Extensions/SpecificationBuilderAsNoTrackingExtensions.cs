using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> AsNoTracking<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder) where TEntity : class
    {
        AsNoTracking<TEntity>(builder);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> AsNoTracking<TEntity>(this ISpecificationBuilder<TEntity> builder)
    {
        builder.Specification.AsNoTracking = true;
        builder.Specification.AsTracking = false;
        builder.Specification.AsNoTrackingWithIdentityResolution = false;

        return builder;
    }
}
