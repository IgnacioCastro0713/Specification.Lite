using Microsoft.EntityFrameworkCore;
using Specification.Lite.Evaluators;
using Specification.Lite.Exceptions;

namespace Specification.Lite;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> WithSpecification<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        query = Evaluator.Instance.GetQuery(query, specification);

        return query;
    }

    public static IQueryable<TResult> WithSpecification<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification)
        where TEntity : class
    {
        return query
            .WithSpecification<TEntity>(specification)
            .Selectors(specification);
    }

    private static IQueryable<TResult> Selectors<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification) where TEntity : class
    {
        if (specification.Selector is null && specification.ManySelector is null)
        {
            throw new SelectorNotFoundException();
        }

        return specification.Selector is not null
            ? query.Select(specification.Selector)
            : query.SelectMany(specification.ManySelector!);
    }

    public static Task<List<TEntity>> ToListAsync<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TEntity> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.ToListAsync(cancellationToken);
    }

    public static Task<List<TResult>> ToListAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TResult> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.ToListAsync(cancellationToken);
    }


    public static Task<TEntity?> FirstOrDefaultAsync<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TEntity> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.FirstOrDefaultAsync(cancellationToken);
    }

    public static Task<TResult?> FirstOrDefaultAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TResult> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.FirstOrDefaultAsync(cancellationToken);
    }

    public static Task<TEntity?> SingleOrDefaultAsync<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TEntity> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.SingleOrDefaultAsync(cancellationToken);
    }

    public static Task<TResult?> SingleOrDefaultAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TResult> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.SingleOrDefaultAsync(cancellationToken);
    }

    public static Task<bool> AnyAsync<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TEntity> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.AnyAsync(cancellationToken);
    }

    public static Task<bool> AnyAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        IQueryable<TResult> queryWithSpec = query.WithSpecification(specification);
        return queryWithSpec.AnyAsync(cancellationToken);
    }
}
