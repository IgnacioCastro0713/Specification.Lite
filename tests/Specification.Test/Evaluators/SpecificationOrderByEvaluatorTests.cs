using API;
using Moq;
using Specification.Lite.Common;
using Specification.Lite.Evaluators;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class SpecificationOrderByEvaluatorTests
{
    [Fact]
    public void ApplyOrderBy_WithNoOrderExpressions_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.OrderByExpressions).Returns([]);

        var entities = new List<TestEntity>
        {
            new() { Id = 2, Name = "Test2" },
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.ApplyOrderBy(mockSpecification.Object);

        // Assert
        Assert.Equal(entities.Count, result.Count());
        // Order should remain the same as original
        Assert.Equal(2, result.First().Id);
    }

    [Fact]
    public void ApplyOrderBy_WithOrderByExpression_SortsQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        var orderExpressions = new List<OrderExpression<TestEntity>>
        {
            new(e => e.Id, OrderType.OrderBy)
        };
        mockSpecification.Setup(s => s.OrderByExpressions).Returns(orderExpressions);

        var entities = new List<TestEntity>
        {
            new() { Id = 3, Name = "Test3" },
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.ApplyOrderBy(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(1, result[0].Id);
        Assert.Equal(2, result[1].Id);
        Assert.Equal(3, result[2].Id);
    }

    [Fact]
    public void ApplyOrderBy_WithOrderByDescendingExpression_SortsQueryDescending()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        var orderExpressions = new List<OrderExpression<TestEntity>>
        {
            new(e => e.Id, OrderType.OrderByDescending)
        };
        mockSpecification.Setup(s => s.OrderByExpressions).Returns(orderExpressions);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.ApplyOrderBy(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(3, result[0].Id);
        Assert.Equal(2, result[1].Id);
        Assert.Equal(1, result[2].Id);
    }

    [Fact]
    public void ApplyOrderBy_WithDuplicateOrderChain_ThrowsException()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        var orderExpressions = new List<OrderExpression<TestEntity>>
        {
            new(e => e.Id, OrderType.OrderBy),
            new(e => e.Name, OrderType.OrderBy) // This should cause an exception
        };
        mockSpecification.Setup(s => s.OrderByExpressions).Returns(orderExpressions);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act & Assert
        Assert.Throws<DuplicateOrderChainException>(() => query.ApplyOrderBy(mockSpecification.Object).ToList());
    }
}
