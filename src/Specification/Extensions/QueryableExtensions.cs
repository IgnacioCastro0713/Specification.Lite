using Microsoft.EntityFrameworkCore;
using Specification.Lite.Evaluators;

namespace Specification.Lite.Extensions;

public static class QueryableExtensions
{
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
