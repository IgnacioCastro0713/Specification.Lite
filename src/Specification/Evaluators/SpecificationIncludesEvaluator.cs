using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Specification.Lite.Expressions;

namespace Specification.Lite.Evaluators;

public static class SpecificationIncludesEvaluator
{
    private static readonly MethodInfo IncludeMethodInfo;
    private static readonly MethodInfo ThenIncludeAfterReferenceMethod;
    private static readonly MethodInfo ThenIncludeAfterCollectionMethod;

    static SpecificationIncludesEvaluator()
    {
        IncludeMethodInfo = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .First(m => m is { Name: "Include", IsGenericMethodDefinition: true } && m.GetParameters().Length == 2);

        var thenIncludeMethods = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m is { Name: "ThenInclude", IsGenericMethodDefinition: true } && m.GetParameters().Length == 2)
            .ToList();

        ThenIncludeAfterReferenceMethod = thenIncludeMethods
            .First(m =>
            {
                ParameterInfo[] parameters = m.GetParameters();
                bool isIIncludableQueryable = parameters[0].ParameterType.IsGenericType &&
                                              parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>);
                if (!isIIncludableQueryable)
                {
                    return false;
                }

                Type previousPropTypeArg = parameters[0].ParameterType.GetGenericArguments()[1];
                return !IsEnumerableType(previousPropTypeArg);
            });

        ThenIncludeAfterCollectionMethod = thenIncludeMethods
            .First(m =>
            {
                ParameterInfo[] parameters = m.GetParameters();
                bool isIIncludableQueryable = parameters[0].ParameterType.IsGenericType &&
                                              parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>);
                if (!isIIncludableQueryable)
                {
                    return false;
                }

                Type previousPropTypeArg = parameters[0].ParameterType.GetGenericArguments()[1];
                return IsEnumerableType(previousPropTypeArg);
            });
    }

    private static bool IsEnumerableType(Type type)
    {
        return type.IsGenericType &&
               (type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                type.GetGenericTypeDefinition() == typeof(List<>));
    }

    public static IQueryable<TEntity> ApplyIncludes<TEntity>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity> specification) where TEntity : class
    {
        IQueryable<TEntity> currentQuery = query;

        foreach (IncludeExpression<TEntity> includePath in specification.IncludeExpressions)
        {
            Type initialPropertyType = includePath.Expression.ReturnType;
            MethodInfo specificIncludeMethod = IncludeMethodInfo.MakeGenericMethod(typeof(TEntity), initialPropertyType);

            object includedQuery = specificIncludeMethod.Invoke(null, [currentQuery, includePath.Expression])!;
            object thenIncludingChain = includedQuery;

            foreach (LambdaExpression thenIncludeLambda in includePath.ThenIncludes)
            {
                Type entityType = typeof(TEntity);

                Type previousPropertyTypeInChain = GetPreviousPropertyTypeFromIncludableQueryable(thenIncludingChain);
                Type currentPropertyTypeInThenInclude = thenIncludeLambda.ReturnType;

                MethodInfo thenIncludeMethodToInvoke;

                if (IsEnumerableType(previousPropertyTypeInChain))
                {
                    Type previousCollectionElementType = previousPropertyTypeInChain.GetGenericArguments()[0];
                    thenIncludeMethodToInvoke = ThenIncludeAfterCollectionMethod.MakeGenericMethod(entityType, previousCollectionElementType, currentPropertyTypeInThenInclude);
                }
                else
                {
                    thenIncludeMethodToInvoke = ThenIncludeAfterReferenceMethod.MakeGenericMethod(entityType, previousPropertyTypeInChain, currentPropertyTypeInThenInclude);
                }

                thenIncludingChain = thenIncludeMethodToInvoke.Invoke(null, [thenIncludingChain, thenIncludeLambda])!;
            }

            currentQuery = (IQueryable<TEntity>)thenIncludingChain;
        }

        return currentQuery;
    }

    private static Type GetPreviousPropertyTypeFromIncludableQueryable(object includableQueryable)
    {
        Type includableType = includableQueryable.GetType();

        Type iIncludableQueryableInterface = includableType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>))
                                             ?? throw new InvalidOperationException($"Could not determine the previous property type for ThenInclude. Object type: {includableType.Name} does not implement IIncludableQueryable<,> or a derived type.");


        return iIncludableQueryableInterface.GetGenericArguments()[1];
    }
}
