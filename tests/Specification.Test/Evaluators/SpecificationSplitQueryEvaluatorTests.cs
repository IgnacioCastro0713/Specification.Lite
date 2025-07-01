using API;
using Moq;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationSplitQueryEvaluatorTests
{
    // Note: Fully testing split query behavior requires an actual DbContext, so we'll focus on logic flow

    [Fact]
    public void ApplySplitQuery_WithNoSplitQuerySpecified_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.IsAsSplitQuery).Returns(false);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.ApplySplitQuery(mockSpecification.Object);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }
}
