using System.Linq.Expressions;

namespace Specification.Lite.Expressions;

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
