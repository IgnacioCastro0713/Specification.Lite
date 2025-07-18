﻿using Microsoft.EntityFrameworkCore;

namespace Specification.Lite.Evaluators;

public sealed class AsTrackingEvaluator : IEvaluator
{
    public static AsTrackingEvaluator Instance { get; } = new();

    public IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.AsTracking)
        {
            query = query.AsTracking();
        }

        return query;
    }
}
