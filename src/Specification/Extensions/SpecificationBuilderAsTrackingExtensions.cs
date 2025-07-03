using Specification.Lite.Builders;
using Specification.Lite.Exceptions;

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
        if (builder.Specification.AsNoTracking)
        {
            throw new ConcurrentTrackingException();
        }

        builder.Specification.AsTracking = true;

        return builder;
    }
}
