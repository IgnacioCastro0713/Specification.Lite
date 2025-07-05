using API;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class EvaluatorTests
{
    [Fact]
    public void GetQuery_ThrowsArgumentNullException_WhenSpecificationIsNull()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            Evaluator.Instance.GetQuery(data, null!)
        );
    }

    [Fact]
    public void GetQuery_ReturnsIQueryable()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var spec = new Specification<TestEntity>();

        // Act
        IQueryable<TestEntity> result = Evaluator.Instance.GetQuery<TestEntity>(data, spec);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
        Assert.Single(result);
        Assert.Equal(1, result.First().Id);
    }

    [Fact]
    public void DummyEvaluator_Implements_IEvaluator()
    {
        var evaluator = new DummyEvaluator();
        Assert.IsAssignableFrom<IEvaluator>(evaluator);
    }
}


public class DummyEvaluator : IEvaluator
{
    public IQueryable<TEntity> Evaluate<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification)
        where TEntity : class
    {
        // Just return the query unchanged for dummy purposes
        return query;
    }
}
