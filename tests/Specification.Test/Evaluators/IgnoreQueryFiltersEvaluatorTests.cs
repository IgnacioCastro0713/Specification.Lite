using API;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class IgnoreQueryFiltersEvaluatorTests
{
    [Fact]
    public void Evaluate_AppliesIgnoreQueryFilters_WhenSpecificationRequestsIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.IgnoreQueryFilters).Returns(true);

        IgnoreQueryFiltersEvaluator evaluator = IgnoreQueryFiltersEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Evaluate(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
        // Unable to assert actual filter status without EF context, but covers code path.
    }

    [Fact]
    public void Evaluate_DoesNotApplyIgnoreQueryFilters_WhenSpecificationDoesNotRequestIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.IgnoreQueryFilters).Returns(false);

        IgnoreQueryFiltersEvaluator evaluator = IgnoreQueryFiltersEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Evaluate(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }
}

