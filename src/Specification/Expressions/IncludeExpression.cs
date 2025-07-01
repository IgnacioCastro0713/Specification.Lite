using System.Linq.Expressions;

namespace Specification.Lite.Expressions;

public class IncludeExpression<TEntity>(LambdaExpression expression)
{
    public LambdaExpression Expression { get; } = expression ?? throw new ArgumentNullException(nameof(expression));

    public List<LambdaExpression> ThenIncludes { get; } = [];

    public IncludeExpression<TEntity> ThenInclude(LambdaExpression expression)
    {
        ThenIncludes.Add(expression);
        return this;
    }
}
