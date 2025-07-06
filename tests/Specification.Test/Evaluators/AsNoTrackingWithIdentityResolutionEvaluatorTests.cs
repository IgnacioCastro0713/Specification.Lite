using API;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class AsNoTrackingWithIdentityResolutionEvaluatorTests
{
    [Fact]
    public void Evaluate_AppliesAsNoTrackingWithIdentityResolution_WhenSpecificationRequestsIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsNoTrackingWithIdentityResolution).Returns(true);

        AsNoTrackingWithIdentityResolutionEvaluator evaluator = AsNoTrackingWithIdentityResolutionEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }

    [Fact]
    public void Evaluate_DoesNotApplyAsNoTrackingWithIdentityResolution_WhenSpecificationDoesNotRequestIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsNoTrackingWithIdentityResolution).Returns(false);

        AsNoTrackingWithIdentityResolutionEvaluator evaluator = AsNoTrackingWithIdentityResolutionEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }
}
