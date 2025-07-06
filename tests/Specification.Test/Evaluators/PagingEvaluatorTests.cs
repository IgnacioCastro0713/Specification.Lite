using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class PagingEvaluatorTests
{
    private sealed class TestEntity { public int Id { get; set; } }

    [Fact]
    public void Evaluate_AppliesSkipAndTake()
    {
        // Arrange
        IQueryable<TestEntity> data = Enumerable.Range(1, 10).Select(i => new TestEntity { Id = i }).AsQueryable();
        var mockSpec = new Specification<TestEntity>()
        {
            Skip = 2,
            Take = 3
        };

        PagingEvaluator evaluator = PagingEvaluator.Instance;

        // Act
        var result = evaluator.Query(data, mockSpec).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(3, result[0].Id);
        Assert.Equal(5, result[2].Id);
    }

    [Fact]
    public void Evaluate_DoesNotSkip_WhenSkipIsZero()
    {
        IQueryable<TestEntity> data = Enumerable.Range(1, 5).Select(i => new TestEntity { Id = i }).AsQueryable();
        var mockSpec = new Specification<TestEntity>()
        {
            Skip = 0,
            Take = 2
        };

        PagingEvaluator evaluator = PagingEvaluator.Instance;

        var result = evaluator.Query(data, mockSpec).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal(2, result[1].Id);
    }

    [Fact]
    public void Evaluate_DoesNotTake_WhenTakeIsNegative()
    {
        IQueryable<TestEntity> data = Enumerable.Range(1, 5).Select(i => new TestEntity { Id = i }).AsQueryable();
        var mockSpec = new Specification<TestEntity>()
        {
            Skip = 1,
            Take = -1 // Negative take should not limit the results
        };


        PagingEvaluator evaluator = PagingEvaluator.Instance;

        var result = evaluator.Query(data, mockSpec).ToList();

        Assert.Equal(4, result.Count); // Only skip is applied
        Assert.Equal(2, result[0].Id);
        Assert.Equal(5, result[3].Id);
    }
}
