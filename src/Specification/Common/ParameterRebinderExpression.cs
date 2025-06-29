using System.Linq.Expressions;

namespace Specification.Lite.Common;

internal class ParameterRebinderExpression : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    internal ParameterRebinderExpression(ParameterExpression parameter) => _parameter = parameter;

    protected override Expression VisitParameter(ParameterExpression node) => _parameter;
}
