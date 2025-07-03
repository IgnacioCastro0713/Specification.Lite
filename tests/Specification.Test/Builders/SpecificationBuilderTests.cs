using API;
using Specification.Lite;
using Specification.Lite.Builders;

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
}
