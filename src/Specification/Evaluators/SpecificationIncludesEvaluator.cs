using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Specification.Lite.Common;

namespace Specification.Lite.Evaluators;

public static class SpecificationIncludesEvaluator
{
    private static readonly MethodInfo IncludeMethodInfo = typeof(EntityFrameworkQueryableExtensions)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .First(m => m.Name == "Include" && m.GetParameters().Length == 2);

    private static readonly Dictionary<bool, MethodInfo> ThenIncludeMethods = GetThenIncludeMethods();

    private static Dictionary<bool, MethodInfo> GetThenIncludeMethods()
    {
        var methods = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == "ThenInclude" && m.GetParameters().Length == 2)
            .ToList();

        var result = new Dictionary<bool, MethodInfo>();

        foreach (MethodInfo method in methods)
        {
            if (!method.IsGenericMethodDefinition ||
                method.GetParameters().Length != 2 ||
                !method.GetParameters()[0].ParameterType.IsGenericType ||
                method.GetParameters()[0].ParameterType.GetGenericTypeDefinition() != typeof(IIncludableQueryable<,>))
            {
                continue;
            }

            Type secondGenericArg = method.GetParameters()[0].ParameterType.GetGenericArguments()[1];

            bool isCollection = secondGenericArg.IsGenericType &&
                                (secondGenericArg.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                                 secondGenericArg.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                                 secondGenericArg.GetGenericTypeDefinition() == typeof(List<>));

            result[isCollection] = method;
        }

        return result;
    }

    public static IQueryable<TEntity> ApplyIncludes<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IQueryable<TEntity> currentQuery = query;

        foreach (IncludeExpression<TEntity> includePath in specification.IncludePaths)
        {
            Type entityType = typeof(TEntity);
            Type propertyType = includePath.Expression.ReturnType;

            MethodInfo includeMethod = IncludeMethodInfo.MakeGenericMethod(entityType, propertyType);
            object? includableQuery = includeMethod.Invoke(null, [currentQuery, includePath.Expression]);

            if (!includePath.ThenIncludes.Any())
            {
                currentQuery = (IQueryable<TEntity>)includableQuery!;
                continue;
            }

            object current = includableQuery!;
            Type currentPropertyType = propertyType;

            for (int i = 0; i < includePath.ThenIncludes.Count; i++)
            {
                LambdaExpression thenIncludeExpression = includePath.ThenIncludes[i];
                bool isCollection = includePath.ThenIncludeIsCollection[i];
                Type nextPropertyType = thenIncludeExpression.ReturnType;
                MethodInfo genericThenIncludeMethod = ThenIncludeMethods[isCollection].MakeGenericMethod(entityType, currentPropertyType, nextPropertyType);
                current = genericThenIncludeMethod.Invoke(null, [current, thenIncludeExpression])!;
                currentPropertyType = nextPropertyType;
            }

            currentQuery = (IQueryable<TEntity>)current;
        }

        return currentQuery;
    }
}
