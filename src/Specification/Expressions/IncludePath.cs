using System.Linq.Expressions;

namespace Specification.Lite.Expressions;

public class IncludePath<TEntity>(Expression<Func<TEntity, object>> expression)
{
    public Expression<Func<TEntity, object>> Expression { get; } = expression ?? throw new ArgumentNullException(nameof(expression));


    public List<LambdaExpression> ThenIncludes { get; } = [];

    public IncludePath<TEntity> AddThenInclude<TProperty, TNavigation>(
        Expression<Func<TProperty, TNavigation>> thenIncludeExpression)
    {
        var convertedSelector = System.Linq.Expressions.Expression.Lambda<Func<TProperty, object>>(
            System.Linq.Expressions.Expression.Convert(thenIncludeExpression.Body, typeof(object)),
            thenIncludeExpression.Parameters);

        ThenIncludes.Add(convertedSelector);
        return this;
    }
}
