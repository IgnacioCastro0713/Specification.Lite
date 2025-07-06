using API;
using Specification.Lite;
using Specification.Lite.Common;
using Specification.Lite.Evaluators;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class OrderEvaluatorTests
{
    [Fact]
    public void Evaluate_AppliesOrderByAscending()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new() { Id = 2, Name = "b" },
            new() { Id = 1, Name = "a" }
        }.AsQueryable();

        var orderExpressions = new List<OrderExpression<TestEntity>>
        {
            new(x => x.Id, OrderType.OrderBy)
        };

        var mockSpec = new Specification<TestEntity>()
        {
            OrderExpressions = orderExpressions
        };


        OrderEvaluator evaluator = OrderEvaluator.Instance;

        // Act
        var result = evaluator.Query(data, mockSpec).ToList();

        // Assert
        Assert.Equal(1, result[0].Id);
        Assert.Equal(2, result[1].Id);
    }

    [Fact]
    public void Evaluate_AppliesOrderByDescending()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new() { Id = 1, Name = "a" },
            new() { Id = 2, Name = "b" }
        }.AsQueryable();

        var orderExpressions = new List<OrderExpression<TestEntity>>
        {
            new(x => x.Id, OrderType.OrderByDescending)
        };

        var mockSpec = new Specification<TestEntity>()
        {
            OrderExpressions = orderExpressions
        };

        OrderEvaluator evaluator = OrderEvaluator.Instance;

        // Act
        var result = evaluator.Query(data, mockSpec).ToList();

        // Assert
        Assert.Equal(2, result[0].Id);
        Assert.Equal(1, result[1].Id);
    }

    [Fact]
    public void Evaluate_AppliesThenBy()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new() { Id = 1, Name = "b" },
            new() { Id = 1, Name = "a" }
        }.AsQueryable();

        var orderExpressions = new List<OrderExpression<TestEntity>>
        {
            new(x => x.Id, OrderType.OrderBy),
            new(x => x.Name, OrderType.ThenBy)
        };

        var mockSpec = new Specification<TestEntity>()
        {
            OrderExpressions = orderExpressions
        };

        OrderEvaluator evaluator = OrderEvaluator.Instance;

        // Act
        var result = evaluator.Query(data, mockSpec).ToList();

        // Assert
        Assert.Equal("a", result[0].Name);
        Assert.Equal("b", result[1].Name);
    }

    [Fact]
    public void Evaluate_ReturnsOriginalQuery_IfNoOrderExpressions()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new() { Id = 2, Name = "b" },
            new() { Id = 1, Name = "a" }
        }.AsQueryable();

        var mockSpec = new Specification<TestEntity>();

        OrderEvaluator evaluator = OrderEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec);

        // Assert
        Assert.Equal(data, result);
    }
}
