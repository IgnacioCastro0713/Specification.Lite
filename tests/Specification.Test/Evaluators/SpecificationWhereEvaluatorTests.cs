using System.Linq.Expressions;
using Moq;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationWhereEvaluatorTests
{
    [Fact]
    public void ApplyCriteria_WithNoExpressions_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.WhereExpressions).Returns([]);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.ApplyWhere(mockSpecification.Object);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public void ApplyCriteria_WithSingleExpression_AppliesFilter()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        var criteriaExp = new List<Expression<Func<TestEntity, bool>>>
        {
            e => e.Id > 1
        };
        mockSpecification.Setup(s => s.WhereExpressions).Returns(criteriaExp);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.ApplyWhere(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.DoesNotContain(result, e => e.Id <= 1);
    }

    [Fact]
    public void ApplyCriteria_WithMultipleExpressions_CombinesFilters()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        var criteriaExp = new List<Expression<Func<TestEntity, bool>>>
        {
            e => e.Id > 1,
            e => e.Name.Contains("t3")
        };
        mockSpecification.Setup(s => s.WhereExpressions).Returns(criteriaExp);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.ApplyWhere(mockSpecification.Object).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(3, result[0].Id);
        Assert.Equal("Test3", result[0].Name);
    }
}