using Specification.Lite.Builders;
using Specification.Lite.Exceptions;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> Take<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        int value) where TEntity : class
    {
        Take<TEntity>(builder, value);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> Take<TEntity>(this ISpecificationBuilder<TEntity> builder, int value)
    {
        if (builder.Specification.Take != -1)
        {
            throw new DuplicateTakeException();
        }

        builder.Specification.Take = value;

        return builder;
    }

    public static ISpecificationBuilder<TEntity, TResult> Skip<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        int value) where TEntity : class
    {
        Skip<TEntity>(builder, value);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> Skip<TEntity>(this ISpecificationBuilder<TEntity> builder, int value)
    {
        if (builder.Specification.Skip != -1)
        {
            throw new DuplicateSkipException();
        }

        builder.Specification.Skip = value;

        return builder;
    }
}
