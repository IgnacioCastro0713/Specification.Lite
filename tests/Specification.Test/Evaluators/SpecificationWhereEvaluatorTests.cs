using API;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationWhereEvaluatorTests
{
    [Fact]
    public void Where_ShouldNotApplyAnyFilter_WhenNoWhereExpressionsAreDefined()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Where(specification);

        // Assert
        var filteredResult = result.ToList();
        Assert.Equal(2, filteredResult.Count);
        Assert.Equal(1, filteredResult[0].Id);
        Assert.Equal(2, filteredResult[1].Id);
    }

    [Fact]
    public void Where_ShouldApplySingleWhereExpression()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id > 1);

        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Where(specification);

        // Assert
        var filteredResult = result.ToList();
        Assert.Single(filteredResult);
        Assert.Equal(2, filteredResult[0].Id);
    }

    [Fact]
    public void Where_ShouldApplyMultipleWhereExpressions()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id > 1);
        specification.Query.Where(e => e.Name.Contains("Entity2"));

        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Where(specification);

        // Assert
        var filteredResult = result.ToList();
        Assert.Single(filteredResult);
        Assert.Equal(2, filteredResult[0].Id);
        Assert.Equal("Entity2", filteredResult[0].Name);
    }

    [Fact]
    public void Where_ShouldNotApplyAnyFilter_ForEntityResultSpecification_WhenNoWhereExpressionsAreDefined()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Where(specification);

        // Assert
        var filteredResult = result.ToList();
        Assert.Equal(2, filteredResult.Count);
        Assert.Equal(1, filteredResult[0].Id);
        Assert.Equal(2, filteredResult[1].Id);
    }

    [Fact]
    public void Where_ShouldApplySingleWhereExpression_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id > 1);

        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Where(specification);

        // Assert
        var filteredResult = result.ToList();
        Assert.Single(filteredResult);
        Assert.Equal(2, filteredResult[0].Id);
    }

    [Fact]
    public void Where_ShouldApplyMultipleWhereExpressions_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id > 1);
        specification.Query.Where(e => e.Name.Contains("Entity2"));

        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Where(specification);

        // Assert
        var filteredResult = result.ToList();
        Assert.Single(filteredResult);
        Assert.Equal(2, filteredResult[0].Id);
        Assert.Equal("Entity2", filteredResult[0].Name);
    }
}
