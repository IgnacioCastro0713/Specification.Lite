using System.Linq.Expressions;
using Specification.Lite.Common;

namespace Specification.Lite.Expressions;

public class IncludeExpression
{
    internal LambdaExpression LambdaExpression { get; }
    internal Type? PreviousPropertyType { get; set; }
    internal IncludeType Type { get; }

    public IncludeExpression(LambdaExpression expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        LambdaExpression = expression;
        PreviousPropertyType = null;
        Type = IncludeType.Include;
    }

    public IncludeExpression(LambdaExpression expression, Type propertyType)
    {
        ArgumentNullException.ThrowIfNull(expression);
        ArgumentNullException.ThrowIfNull(propertyType);

        LambdaExpression = expression;
        PreviousPropertyType = propertyType;
        Type = IncludeType.ThenInclude;
    }
}
