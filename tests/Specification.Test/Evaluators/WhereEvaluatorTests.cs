using System.Linq.Expressions;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class WhereEvaluatorTests
{
    private sealed class TestEntity { public int Id { get; set; } public string Name { get; set; } }

    [Fact]
    public void Evaluate_AppliesSinglePredicate()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = "a" },
            new TestEntity { Id = 2, Name = "b" },
            new TestEntity { Id = 3, Name = "a" }
        }.AsQueryable();

        var whereExpressions = new List<Expression<Func<TestEntity, bool>>>
        {
            x => x.Name == "a"
        };

        var mockSpec = new Specification<TestEntity>()
        {
            WhereExpressions = whereExpressions
        };

        var evaluator = new WhereEvaluator();

        // Act
        var result = evaluator.Evaluate(data, mockSpec).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, x => Assert.Equal("a", x.Name));
    }

    [Fact]
    public void Evaluate_AppliesMultiplePredicates()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = "a" },
            new TestEntity { Id = 2, Name = "b" },
            new TestEntity { Id = 3, Name = "a" }
        }.AsQueryable();

        var whereExpressions = new List<Expression<Func<TestEntity, bool>>>
        {
            x => x.Name == "a",
            x => x.Id > 1
        };

        var mockSpec = new Specification<TestEntity>
        {
            WhereExpressions = whereExpressions
        };
        var evaluator = new WhereEvaluator();

        // Act
        var result = evaluator.Evaluate(data, mockSpec).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(3, result[0].Id);
    }

    [Fact]
    public void Evaluate_ReturnsOriginalQuery_IfNoWhereExpressions()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = "a" },
            new TestEntity { Id = 2, Name = "b" }
        }.AsQueryable();

        var mockSpec = new Specification<TestEntity>();

        var evaluator = new WhereEvaluator();

        // Act
        IQueryable<TestEntity> result = evaluator.Evaluate(data, mockSpec);

        // Assert
        Assert.Equal(data, result);
    }
}
