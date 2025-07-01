using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Builders;

public class IncludeBuilder<TEntity, TProperty>
{
    private readonly IncludeExpression<TEntity> _includeExpression;
    internal IncludeBuilder(IncludeExpression<TEntity> includeExpression) => _includeExpression = includeExpression;

    public IncludeBuilder<TEntity, TNavigation> ThenInclude<TNavigation>(
        Expression<Func<TProperty, TNavigation>> thenIncludeExpression)
    {
        _includeExpression.ThenInclude(thenIncludeExpression);
        return new IncludeBuilder<TEntity, TNavigation>(_includeExpression);
    }

    public IncludeBuilder<TEntity, TElement> ThenInclude<TElement>(
        Expression<Func<TProperty, IEnumerable<TElement>>> thenIncludeExpression)
    {
        _includeExpression.ThenInclude(thenIncludeExpression);
        return new IncludeBuilder<TEntity, TElement>(_includeExpression);
    }
}
