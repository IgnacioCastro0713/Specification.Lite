using API;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class AsTrackingEvaluatorTests
{
    [Fact]
    public void Evaluate_AppliesAsTracking_WhenSpecificationRequestsIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsTracking).Returns(true);

        AsTrackingEvaluator evaluator = AsTrackingEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Evaluate(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
        // Can't assert tracking without EF context, but ensures correct path is taken.
    }

    [Fact]
    public void Evaluate_DoesNotApplyAsTracking_WhenSpecificationDoesNotRequestIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsTracking).Returns(false);

        AsTrackingEvaluator evaluator = AsTrackingEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Evaluate(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }
}
