using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Specification.Lite.Common;
using IncludeExpression = Specification.Lite.Expressions.IncludeExpression;

namespace Specification.Lite.Evaluators;

public class SpecificationIncludesEvaluator : IEvaluator
{
    private static readonly MethodInfo IncludeMethodInfo = typeof(EntityFrameworkQueryableExtensions)
        .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.Include))
        .Single(methodInfo => methodInfo.IsPublic && methodInfo.GetGenericArguments().Length == 2
                                  && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)
                                  && methodInfo.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo = typeof(EntityFrameworkQueryableExtensions)
            .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Single(methodInfo => methodInfo.IsPublic && methodInfo.GetGenericArguments().Length == 3
                                      && methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter
                                      && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
                                      && methodInfo.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    private static readonly MethodInfo ThenIncludeAfterEnumerableMethodInfo = typeof(EntityFrameworkQueryableExtensions)
            .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Single(methodInfo => methodInfo.IsPublic && methodInfo.GetGenericArguments().Length == 3
                                      && !methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter
                                      && methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IIncludableQueryable<,>)
                                      && methodInfo.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

    public IQueryable<TEntity> Evaluate<TEntity>(
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

    private static IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> query, IncludeExpression includeExpression) where TEntity : class
    {
        ParameterExpression queryableParameter = Expression.Parameter(typeof(IQueryable));
        ParameterExpression lambdaParameter = Expression.Parameter(typeof(LambdaExpression));

        MethodCallExpression call = Expression.Call(
            IncludeMethodInfo.MakeGenericMethod(typeof(TEntity), includeExpression.LambdaExpression.ReturnType),
            Expression.Convert(queryableParameter, typeof(IQueryable<>).MakeGenericType(typeof(TEntity))),
            Expression.Convert(lambdaParameter, typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(typeof(TEntity), includeExpression.LambdaExpression.ReturnType))));

        var lambda = Expression.Lambda<Func<IQueryable, LambdaExpression, IQueryable>>(call, queryableParameter, lambdaParameter);
        Func<IQueryable, LambdaExpression, IQueryable> compiledLambda = lambda.Compile();
        query = (IQueryable<TEntity>)compiledLambda(query, includeExpression.LambdaExpression);

        return query;
    }

    private static IQueryable<TEntity> ThenInclude<TEntity>(IQueryable<TEntity> query, IncludeExpression includeExpression)
        where TEntity : class
    {
        bool isEnumerable = IsGenericEnumerable(includeExpression.PreviousPropertyType!, out Type previousPropertyType);
        MethodInfo methodInfo = isEnumerable ? ThenIncludeAfterEnumerableMethodInfo : ThenIncludeAfterReferenceMethodInfo;

        ParameterExpression queryableParameter = Expression.Parameter(typeof(IQueryable));
        ParameterExpression lambdaParameter = Expression.Parameter(typeof(LambdaExpression));

        MethodCallExpression call = Expression.Call(
            methodInfo.MakeGenericMethod(typeof(TEntity), previousPropertyType, includeExpression.LambdaExpression.ReturnType),
            Expression.Convert(queryableParameter, typeof(IIncludableQueryable<,>).MakeGenericType(typeof(TEntity), includeExpression.PreviousPropertyType!)),
            Expression.Convert(lambdaParameter, typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(previousPropertyType, includeExpression.LambdaExpression.ReturnType))));

        var lambda = Expression.Lambda<Func<IQueryable, LambdaExpression, IQueryable>>(call, queryableParameter, lambdaParameter);
        Func<IQueryable, LambdaExpression, IQueryable> compiledLambda = lambda.Compile();
        query = (IQueryable<TEntity>)compiledLambda(query, includeExpression.LambdaExpression);

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
