using System.Linq.Expressions;

namespace Specification.Lite.Common;

internal class ParameterRebinder : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    internal ParameterRebinder(ParameterExpression parameter) => _parameter = parameter;

    protected override Expression VisitParameter(ParameterExpression node) => _parameter;
}
