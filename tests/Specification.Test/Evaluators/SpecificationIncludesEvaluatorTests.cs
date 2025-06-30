using Moq;
using Specification.Lite.Evaluators;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class SpecificationIncludesEvaluatorTests
{
    // These classes are just for testing the ThenInclude functionality
    public class TestEntityWithRelation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TestRelatedEntity? Related { get; set; }
    }

    public class TestRelatedEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public TestNestedEntity? Nested { get; set; }
    }

    public class TestNestedEntity
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    // Note: Testing includes fully would require an actual DbContext, so I'll use a simplified approach here
    [Fact]
    public void ApplyIncludes_WithNoIncludes_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.IncludeExpressions).Returns([]);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.ApplyIncludes(mockSpecification.Object);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }

    // Include functionality primarily depends on EF Core, so we'll verify the setup but not the execution
    [Fact]
    public void ApplyIncludes_WithIncludePath_SetupCorrectly()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntityWithRelation>>();
        var includePath = new IncludeExpression<TestEntityWithRelation>(e => e.Related!);
        mockSpecification.Setup(s => s.IncludeExpressions).Returns([includePath]);

        // No actual query execution here since we can't truly test includes without a DbContext
        Assert.NotNull(includePath.Expression);
        Assert.Empty(includePath.ThenIncludes);
    }

    [Fact]
    public void ApplyIncludes_WithThenIncludes_SetupCorrectly()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntityWithRelation>>();

        // Create include path with then includes
        var includePath = new IncludeExpression<TestEntityWithRelation>(e => e.Related!);
        includePath.AddThenInclude<TestRelatedEntity, TestNestedEntity>(r => r.Nested!);

        mockSpecification.Setup(s => s.IncludeExpressions).Returns([includePath]);

        // Assert
        Assert.NotNull(includePath.Expression);
        Assert.Single(includePath.ThenIncludes);
    }

    [Fact]
    public void IncludePath_AddThenInclude_AddsThenIncludeToList()
    {
        // Arrange
        var includePath = new IncludeExpression<TestEntityWithRelation>(e => e.Related!);

        // Act
        includePath.AddThenInclude<TestRelatedEntity, TestNestedEntity>(r => r.Nested!);

        // Assert
        Assert.Single(includePath.ThenIncludes);
    }

    [Fact]
    public void IncludePath_AddThenInclude_ReturnsIncludePath()
    {
        // Arrange
        var includePath = new IncludeExpression<TestEntityWithRelation>(e => e.Related!);

        // Act
        IncludeExpression<TestEntityWithRelation> result = includePath.AddThenInclude<TestRelatedEntity, TestNestedEntity>(r => r.Nested!);

        // Assert
        Assert.Same(includePath, result);
    }

    [Fact]
    public void IncludePath_AddThenInclude_CanBeChained()
    {
        // Arrange
        var includePath = new IncludeExpression<TestEntityWithRelation>(e => e.Related!);

        // Act - chain multiple ThenIncludes (this mimics the real-world usage)
        includePath
            .AddThenInclude<TestRelatedEntity, string>(r => r.Description)
            .AddThenInclude<TestRelatedEntity, TestNestedEntity>(r => r.Nested!);

        // Assert
        Assert.Equal(2, includePath.ThenIncludes.Count);
    }

    [Fact]
    public void IncludePath_Constructor_ThrowsOnNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new IncludeExpression<TestEntityWithRelation>(null!));
    }
}