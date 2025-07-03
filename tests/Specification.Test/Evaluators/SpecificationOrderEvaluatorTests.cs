using API;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationOrderEvaluatorTests
{
    [Fact]
    public void Order_ShouldApplySingleOrderBy()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.OrderBy(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "A" },
            new() { Id = 3, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("A", orderedResult[0].Name);
        Assert.Equal("B", orderedResult[1].Name);
        Assert.Equal("C", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplySingleOrderByDescending()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.OrderByDescending(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "A" },
            new() { Id = 3, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("C", orderedResult[0].Name);
        Assert.Equal("B", orderedResult[1].Name);
        Assert.Equal("A", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplyThenBy()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.OrderBy(e => e.Id).ThenBy(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 1, Name = "A" },
            new() { Id = 2, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("A", orderedResult[0].Name);
        Assert.Equal("B", orderedResult[1].Name);
        Assert.Equal("C", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplyThenByDescending()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.OrderBy(e => e.Id).ThenByDescending(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "A" },
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("B", orderedResult[0].Name);
        Assert.Equal("A", orderedResult[1].Name);
        Assert.Equal("C", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplyMixedOrderByAndThenBy()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.OrderByDescending(e => e.Name).ThenBy(e => e.Id);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "A" },
            new() { Id = 3, Name = "B" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal(1, orderedResult[0].Id); // Name = "B"
        Assert.Equal(3, orderedResult[1].Id); // Name = "B"
        Assert.Equal(2, orderedResult[2].Id); // Name = "A"
    }

    [Fact]
    public void Order_ShouldApplySingleOrderBy_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.OrderBy(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "A" },
            new() { Id = 3, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("A", orderedResult[0].Name);
        Assert.Equal("B", orderedResult[1].Name);
        Assert.Equal("C", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplySingleOrderByDescending_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.OrderByDescending(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "A" },
            new() { Id = 3, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("C", orderedResult[0].Name);
        Assert.Equal("B", orderedResult[1].Name);
        Assert.Equal("A", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplyThenBy_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.OrderBy(e => e.Id).ThenBy(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 1, Name = "A" },
            new() { Id = 2, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("A", orderedResult[0].Name);
        Assert.Equal("B", orderedResult[1].Name);
        Assert.Equal("C", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplyThenByDescending_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.OrderBy(e => e.Id).ThenByDescending(e => e.Name);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "A" },
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "C" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal("B", orderedResult[0].Name);
        Assert.Equal("A", orderedResult[1].Name);
        Assert.Equal("C", orderedResult[2].Name);
    }

    [Fact]
    public void Order_ShouldApplyMixedOrderByAndThenBy_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.OrderByDescending(e => e.Name).ThenBy(e => e.Id);

        // Act
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "B" },
            new() { Id = 2, Name = "A" },
            new() { Id = 3, Name = "B" }
        }.AsQueryable();

        IQueryable<TestEntity> result = query.Order(specification);

        // Assert
        var orderedResult = result.ToList();
        Assert.Equal(1, orderedResult[0].Id); // Name = "B"
        Assert.Equal(3, orderedResult[1].Id); // Name = "B"
        Assert.Equal(2, orderedResult[2].Id); // Name = "A"
    }
}
