using System.Linq.Expressions;
using Specification.Lite.Expressions;

namespace Specification.Lite.Builders;

public class IncludeBuilder<TEntity, TProperty>(IncludeExpression<TEntity> includeExpression)
{
    public IncludeBuilder<TEntity, TNavigation> ThenInclude<TNavigation>(
        Expression<Func<TProperty, TNavigation>> thenIncludeExpression)
    {
        includeExpression.ThenInclude(thenIncludeExpression);
        return new IncludeBuilder<TEntity, TNavigation>(includeExpression);
    }

    public IncludeBuilder<TEntity, TElement> ThenInclude<TElement>(
        Expression<Func<TProperty, IEnumerable<TElement>>> thenIncludeExpression)
    {
        includeExpression.ThenInclude(thenIncludeExpression);
        return new IncludeBuilder<TEntity, TElement>(includeExpression);
    }
}
