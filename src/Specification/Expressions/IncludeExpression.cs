using System.Linq.Expressions;
using Specification.Lite.Common;

namespace Specification.Lite.Expressions;

public class IncludeExpression
{
    public LambdaExpression LambdaExpression { get; }
    public Type? PreviousPropertyType { get; set; }
    public IncludeType Type { get; }

    public IncludeExpression(LambdaExpression expression)
    {
        LambdaExpression = expression ?? throw new ArgumentNullException(nameof(expression));
        PreviousPropertyType = null;
        Type = IncludeType.Include;
    }

    public IncludeExpression(LambdaExpression expression, Type propertyType)
    {
        LambdaExpression = expression ?? throw new ArgumentNullException(nameof(expression));
        PreviousPropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        Type = IncludeType.ThenInclude;
    }
}
