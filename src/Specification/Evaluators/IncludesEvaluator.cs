﻿using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Specification.Lite.Common;
using IncludeExpression = Specification.Lite.Expressions.IncludeExpression;

namespace Specification.Lite.Evaluators;

public sealed class IncludesEvaluator : IEvaluator
{
    public static IncludesEvaluator Instance { get; } = new();

    private static readonly MethodInfo IncludeMethodInfo = typeof(EntityFrameworkQueryableExtensions)
        .GetTypeInfo()
        .GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.Include))
        .Single(methodInfo => methodInfo.IsPublic && methodInfo.GetGenericArguments().Length == 2
                                  && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)
                                  && methodInfo.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo = typeof(EntityFrameworkQueryableExtensions)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Single(methodInfo => methodInfo.IsPublic && methodInfo.GetGenericArguments().Length == 3
                                      && methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter
                                      && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
                                      && methodInfo.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private static readonly MethodInfo ThenIncludeAfterEnumerableMethodInfo = typeof(EntityFrameworkQueryableExtensions)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Single(methodInfo => methodInfo.IsPublic && methodInfo.GetGenericArguments().Length == 3
                                      && !methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter
                                      && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
                                      && methodInfo.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private sealed record CacheKey(IncludeType IncludeType, Type EntityType, Type PropertyType, Type? PreviousPropertyType = null);
    private static readonly ConcurrentDictionary<CacheKey, Func<IQueryable, LambdaExpression, IQueryable>> IncludeCache = new();

    public IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        return specification.IncludeExpressions.Aggregate(query, (current, includeExpression) => includeExpression.Type switch
        {
            IncludeType.Include => Include(current, includeExpression),
            IncludeType.ThenInclude => ThenInclude(current, includeExpression),
            _ => current
        });
    }

    private static IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> query, IncludeExpression includeExpression)
        where TEntity : class
    {
        var key = new CacheKey(IncludeType.Include, typeof(TEntity), includeExpression.LambdaExpression.ReturnType);
        Func<IQueryable, LambdaExpression, IQueryable> del = IncludeCache.GetOrAdd(key, cacheKey =>
        {
            ParameterExpression queryableParameter = Expression.Parameter(typeof(IQueryable));
            ParameterExpression lambdaParameter = Expression.Parameter(typeof(LambdaExpression));
            MethodCallExpression call = Expression.Call(
                IncludeMethodInfo.MakeGenericMethod(cacheKey.EntityType, cacheKey.PropertyType),
                Expression.Convert(queryableParameter, typeof(IQueryable<>).MakeGenericType(cacheKey.EntityType)),
                Expression.Convert(lambdaParameter, typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(cacheKey.EntityType, cacheKey.PropertyType))));
            var lambda = Expression.Lambda<Func<IQueryable, LambdaExpression, IQueryable>>(call, queryableParameter, lambdaParameter);

            return lambda.Compile();
        });
        query = (IQueryable<TEntity>)del(query, includeExpression.LambdaExpression);

        return query;
    }

    private static IQueryable<TEntity> ThenInclude<TEntity>(IQueryable<TEntity> query, IncludeExpression includeExpression)
        where TEntity : class
    {
        bool isEnumerable = IsGenericEnumerable(includeExpression.PreviousPropertyType!, out Type previousPropertyType);
        MethodInfo methodInfo = isEnumerable ? ThenIncludeAfterEnumerableMethodInfo : ThenIncludeAfterReferenceMethodInfo;
        var key = new CacheKey(IncludeType.ThenInclude, typeof(TEntity), includeExpression.LambdaExpression.ReturnType, includeExpression.PreviousPropertyType);
        Func<IQueryable, LambdaExpression, IQueryable> del = IncludeCache.GetOrAdd(key, cacheKey =>
        {
            ParameterExpression queryableParameter = Expression.Parameter(typeof(IQueryable));
            ParameterExpression lambdaParameter = Expression.Parameter(typeof(LambdaExpression));
            MethodCallExpression call = Expression.Call(
                methodInfo.MakeGenericMethod(cacheKey.EntityType, previousPropertyType, cacheKey.PropertyType),
                Expression.Convert(queryableParameter, typeof(IIncludableQueryable<,>).MakeGenericType(cacheKey.EntityType, cacheKey.PreviousPropertyType!)),
                Expression.Convert(lambdaParameter, typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(previousPropertyType, cacheKey.PropertyType))));
            var lambda = Expression.Lambda<Func<IQueryable, LambdaExpression, IQueryable>>(call, queryableParameter, lambdaParameter);

            return lambda.Compile();
        });
        query = (IQueryable<TEntity>)del(query, includeExpression.LambdaExpression);

        return query;
    }

    private static bool IsGenericEnumerable(Type type, out Type propertyType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            propertyType = type.GenericTypeArguments[0];
            return true;
        }

        propertyType = type;
        return false;
    }
}
