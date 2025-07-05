using System.Linq.Expressions;
using Specification.Lite;
using Specification.Lite.Common;

namespace Specification.Test.Extensions;

public class SpecificationBuilderOrderExtensionsTests
{
    private sealed class Dummy { public int Value { get; set; } }

    [Fact]
    public void OrderBy_Should_AddOrderByExpression()
    {
        var specification = new Specification<Dummy>();
        Expression<Func<Dummy, object?>> expr = d => d.Value;

        specification.Query.OrderBy(expr);

        Assert.Contains(specification.OrderExpressions, o => o.KeySelector == expr && o.OrderType == OrderType.OrderBy);
    }

    [Fact]
    public void ThenBy_Should_AddThenByExpression()
    {
        var specification = new Specification<Dummy>();
        Expression<Func<Dummy, object?>> expr = d => d.Value;

        specification.Query.OrderBy(expr).ThenBy(expr);

        Assert.Contains(specification.OrderExpressions, o => o.KeySelector == expr && o.OrderType == OrderType.ThenBy);
    }

    [Fact]
    public void OrderByDescending_Should_AddOrderByDescendingExpression()
    {
        var specification = new Specification<Dummy>();
        Expression<Func<Dummy, object?>> expr = d => d.Value;

        specification.Query.OrderBy(expr).OrderByDescending(expr);

        Assert.Contains(specification.OrderExpressions, o => o.KeySelector == expr && o.OrderType == OrderType.OrderByDescending);
    }

    [Fact]
    public void ThenByDescending_Should_AddThenByDescendingExpression()
    {
        var specification = new Specification<Dummy>();
        Expression<Func<Dummy, object?>> expr = d => d.Value;

        specification.Query.OrderBy(expr).ThenByDescending(expr);

        Assert.Contains(specification.OrderExpressions, o => o.KeySelector == expr && o.OrderType == OrderType.ThenByDescending);
    }

}
