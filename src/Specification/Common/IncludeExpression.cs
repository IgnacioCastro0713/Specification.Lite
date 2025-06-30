using System.Linq.Expressions;

namespace Specification.Lite.Common;

public class IncludeExpression<TEntity>(LambdaExpression expression, bool isCollection)
{
    public LambdaExpression Expression { get; } = expression ?? throw new ArgumentNullException(nameof(expression));

    public bool IsCollection { get; } = isCollection;

    public List<LambdaExpression> ThenIncludes { get; } = [];

    public List<bool> ThenIncludeIsCollection { get; } = [];

    public IncludeExpression<TEntity> ThenInclude(LambdaExpression expression, bool isCollection)
    {
        ThenIncludes.Add(expression);
        ThenIncludeIsCollection.Add(isCollection);
        return this;
    }
}


public class IncludeBuilder<TEntity, TProperty>
{
    private readonly IncludeExpression<TEntity> _includeExpression;

    internal IncludeBuilder(IncludeExpression<TEntity> includeExpression) => _includeExpression = includeExpression;

    public IncludeBuilder<TEntity, TNavigation> ThenInclude<TNavigation>(
        Expression<Func<TProperty, TNavigation>> thenIncludeExpression)
    {
        _includeExpression.ThenInclude(thenIncludeExpression, false);
        return new IncludeBuilder<TEntity, TNavigation>(_includeExpression);
    }

    public IncludeBuilder<TEntity, TElement> ThenInclude<TElement>(
        Expression<Func<TProperty, IEnumerable<TElement>>> thenIncludeExpression)
    {
        _includeExpression.ThenInclude(thenIncludeExpression, true);
        return new IncludeBuilder<TEntity, TElement>(_includeExpression);
    }
}
