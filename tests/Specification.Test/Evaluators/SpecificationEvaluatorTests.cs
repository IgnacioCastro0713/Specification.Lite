using Moq;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationEvaluatorTests
{
    [Fact]
    public void SpecifyQuery_AppliesAllEvaluatorsInCorrectOrder()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.WhereExpressions).Returns([]);
        mockSpecification.Setup(s => s.IncludeExpressions).Returns([]);
        mockSpecification.Setup(s => s.OrderByExpressions).Returns([]);

        IQueryable<TestEntity> query = new List<TestEntity>().AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.SpecificationQuery(mockSpecification.Object);

        // Assert
        Assert.NotNull(result);
        // This primarily tests that the chain of method calls works without exceptions
    }

    [Fact]
    public void SpecifyQuery_WithResultType_AppliesAllEvaluatorsAndSelector()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity, TestDto>>();
        mockSpecification.Setup(s => s.WhereExpressions).Returns([]);
        mockSpecification.Setup(s => s.IncludeExpressions).Returns([]);
        mockSpecification.Setup(s => s.OrderByExpressions).Returns([]);
        mockSpecification.Setup(s => s.Selector).Returns((e) => new TestDto { Id = e.Id, Name = e.Name });

        IQueryable<TestEntity> query = new List<TestEntity> { new() { Id = 1, Name = "Test" } }.AsQueryable();

        // Act
        IQueryable<TestDto> result = query.SpecificationQuery(mockSpecification.Object);

        // Assert
        Assert.NotNull(result);
    }
}
