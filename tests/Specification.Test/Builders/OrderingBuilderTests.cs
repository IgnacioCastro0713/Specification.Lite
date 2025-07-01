using System.Linq.Expressions;
using API;
using Specification.Lite.Builders;
using Specification.Lite.Common;
using Specification.Lite.Expressions;

namespace Specification.Test.Builders;

public class OrderingBuilderTests
{
    [Fact]
    public void ThenBy_AddsThenByExpression()
    {
        // Arrange
        var orderExpression = new OrderExpression<TestEntity>(e => e.Id, OrderTypeEnum.OrderBy);
        var builder = new OrderingBuilder<TestEntity>(orderExpression);

        Expression<Func<TestEntity, object>> thenByExpression = e => e.Name;

        // Act
        OrderingBuilder<TestEntity> resultBuilder = builder.ThenBy(thenByExpression);

        // Assert
        Assert.NotNull(resultBuilder);
        Assert.Single(orderExpression.ThenOrders);
    }

    [Fact]
    public void ThenByDescending_AddsThenByDescendingExpression()
    {
        // Arrange
        var orderExpression = new OrderExpression<TestEntity>(e => e.Id, OrderTypeEnum.OrderBy);
        var builder = new OrderingBuilder<TestEntity>(orderExpression);
        Expression<Func<TestEntity, object>> thenByDescendingExpression = e => e.Name;

        // Act
        OrderingBuilder<TestEntity> result = builder.ThenByDescending(thenByDescendingExpression);

        // Assert
        Assert.NotNull(result);
        Assert.Single(orderExpression.ThenOrders);
    }
}
