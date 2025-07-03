using Specification.Lite.Builders;
using Specification.Lite.Exceptions;

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
        if (builder.Specification.AsTracking)
        {
            throw new ConcurrentTrackingException();
        }

        builder.Specification.AsNoTracking = true;

        return builder;
    }
}
