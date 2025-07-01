using API;
using Moq;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationIncludesEvaluatorTests
{
    // These classes are just for testing the ThenInclude functionality

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








}
