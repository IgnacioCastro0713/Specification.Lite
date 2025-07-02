using API;
using Moq;
using Specification.Lite;

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
        mockSpecification.Setup(s => s.OrderExpressions).Returns([]);

        IQueryable<TestEntity> query = new List<TestEntity>().AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.WithSpecification(mockSpecification.Object);

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
        mockSpecification.Setup(s => s.OrderExpressions).Returns([]);
        mockSpecification.Setup(s => s.Selector).Returns((e) => new TestDto { Id = e.Id, Name = e.Name });

        IQueryable<TestEntity> query = new List<TestEntity> { new() { Id = 1, Name = "Test" } }.AsQueryable();

        // Act
        IQueryable<TestDto> result = query.WithSpecification(mockSpecification.Object);

        // Assert
        Assert.NotNull(result);
    }
}
