using API;
using Specification.Lite;
using Specification.Lite.Builders;
using Specification.Lite.Common;
using Specification.Lite.Expressions;

namespace Specification.Test.Builders;

public class SpecificationBuilderTests
{
    [Fact]
    public void SpecificationBuilder_ShouldInitializeWithSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity>();

        // Act
        var builder = new SpecificationBuilder<TestEntity>(specification);

        // Assert
        Assert.NotNull(builder.Specification);
        Assert.Equal(specification, builder.Specification);
    }

    [Fact]
    public void SpecificationBuilderWithResult_ShouldInitializeWithSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();

        // Act
        var builder = new SpecificationBuilder<TestEntity, TestDto>(specification);

        // Assert
        Assert.NotNull(builder.Specification);
        Assert.Equal(specification, builder.Specification);
    }

    [Fact]
    public void SpecificationBuilderWithResult_ShouldInheritFromBaseBuilder()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();

        // Act
        var builder = new SpecificationBuilder<TestEntity, TestDto>(specification);

        // Assert
        Assert.IsAssignableFrom<SpecificationBuilder<TestEntity>>(builder);
    }

    [Fact]
    public void SpecificationBuilder_ShouldImplementISpecificationBuilder()
    {
        // Arrange
        var specification = new Specification<TestEntity>();

        // Act
        var builder = new SpecificationBuilder<TestEntity>(specification);

        // Assert
        Assert.IsAssignableFrom<ISpecificationBuilder<TestEntity>>(builder);
    }

    [Fact]
    public void SpecificationBuilderWithResult_ShouldImplementISpecificationBuilderWithResult()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();

        // Act
        var builder = new SpecificationBuilder<TestEntity, TestDto>(specification);

        // Assert
        Assert.IsAssignableFrom<ISpecificationBuilder<TestEntity, TestDto>>(builder);
    }

    [Fact]
    public void SpecificationBuilder_ShouldAddOrderExpression()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var builder = new SpecificationBuilder<TestEntity>(specification);

        // Act
        builder.Specification.OrderExpressions.Add(new OrderExpression<TestEntity>(e => e.Name, OrderType.OrderBy));

        // Assert
        Assert.Single(specification.OrderExpressions);
        Assert.Equal(OrderType.OrderBy, specification.OrderExpressions.First().OrderType);
        Assert.Equal("e => e.Name", specification.OrderExpressions.First().KeySelector.ToString());
    }

    [Fact]
    public void SpecificationBuilderWithResult_ShouldAddOrderExpression()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        var builder = new SpecificationBuilder<TestEntity, TestDto>(specification);

        // Act
        builder.Specification.OrderExpressions.Add(new OrderExpression<TestEntity>(e => e.Id, OrderType.OrderByDescending));

        // Assert
        Assert.Single(specification.OrderExpressions);
        Assert.Equal(OrderType.OrderByDescending, specification.OrderExpressions.First().OrderType);
        Assert.Equal("e => Convert(e.Id, Object)", specification.OrderExpressions.First().KeySelector.ToString());
    }
}
