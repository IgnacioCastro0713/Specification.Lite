using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public static class SpecificationIncludesEvaluator
{
    public static IQueryable<TEntity> ApplyIncludes<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IQueryable<TEntity> currentQuery = query;

        foreach (IncludePath<TEntity> includePath in specification.IncludePaths)
        {
            IIncludableQueryable<TEntity, object> includable = currentQuery.Include(includePath.Expression);

            if (!includePath.ThenIncludes.Any())
            {
                currentQuery = includable;
                continue;
            }

            currentQuery = ApplyThenIncludes(
                includable,
                typeof(TEntity),
                includePath.Expression.Body.Type,
                includePath.ThenIncludes);
        }

        return currentQuery;
    }

    private static IQueryable<TEntity> ApplyThenIncludes<TEntity>(
        IIncludableQueryable<TEntity, object> includable,
        Type rootEntityType,
        Type previousPropertyType,
        List<LambdaExpression> thenIncludes)
        where TEntity : class
    {
        MethodInfo thenIncludeMethod = GetThenIncludeMethod();
        object current = includable;
        Type currentPropType = previousPropertyType;

        foreach (LambdaExpression thenInclude in thenIncludes)
        {
            Type nextPropertyType = thenInclude.Body.Type;

            MethodInfo genericMethod = thenIncludeMethod.MakeGenericMethod(
                rootEntityType,
                currentPropType,
                nextPropertyType);

            current = genericMethod.Invoke(null, [current, thenInclude])!;
            currentPropType = nextPropertyType;
        }

        return (IQueryable<TEntity>)current;
    }

    private static MethodInfo GetThenIncludeMethod()
    {
        return typeof(EntityFrameworkQueryableExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Where(m => m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 3)
            .Where(m => m.GetParameters().Length == 2)
            .First(m => m.GetParameters()[0].ParameterType.IsGenericType &&
                        m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() ==
                        typeof(IIncludableQueryable<,>));
    }
}