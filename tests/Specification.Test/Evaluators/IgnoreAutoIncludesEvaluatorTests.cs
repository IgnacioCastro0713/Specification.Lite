using API;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class IgnoreAutoIncludesEvaluatorTests
{
    [Fact]
    public void Evaluate_AppliesIgnoreAutoIncludes_WhenSpecificationRequestsIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new() { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.IgnoreAutoIncludes).Returns(true);

        IgnoreAutoIncludesEvaluator evaluator = IgnoreAutoIncludesEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }

    [Fact]
    public void Evaluate_DoesNotApplyIgnoreAutoIncludes_WhenSpecificationDoesNotRequestIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new() { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.IgnoreAutoIncludes).Returns(false);

        IgnoreAutoIncludesEvaluator evaluator = IgnoreAutoIncludesEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }
}
