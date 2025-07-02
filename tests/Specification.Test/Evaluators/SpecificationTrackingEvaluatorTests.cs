using API;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationTrackingEvaluatorTests
{
    // Note: Fully testing tracking behavior requires an actual DbContext, so we'll focus on logic flow
    public sealed class TestSpecification : Specification<TestEntity>;

    [Fact]
    public void ApplyTracking_WithNoTrackingSpecified_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.IsAsNoTracking).Returns(false);
        mockSpecification.Setup(s => s.IsAsTracking).Returns(false);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Tracking(mockSpecification.Object);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }



}
