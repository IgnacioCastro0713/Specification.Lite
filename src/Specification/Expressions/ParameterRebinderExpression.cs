using System.Linq.Expressions;

namespace Specification.Lite.Expressions;

internal sealed class ParameterRebinder : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    internal ParameterRebinder(ParameterExpression parameter) => _parameter = parameter;

    protected override Expression VisitParameter(ParameterExpression node) => _parameter;
}
