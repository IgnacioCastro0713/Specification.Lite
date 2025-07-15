using Specification.Lite.Builders;

namespace Specification.Lite;

public static partial class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<TEntity, TResult> TagWith<TEntity, TResult>(
        this ISpecificationBuilder<TEntity, TResult> builder,
        string tag) where TEntity : class
    {
        TagWith<TEntity>(builder, tag);
        return builder;
    }

    public static ISpecificationBuilder<TEntity> TagWith<TEntity>(
        this ISpecificationBuilder<TEntity> builder,
        string tag)
    {
        builder.Specification.QueryTags.Add(tag);

        return builder;
    }
}
