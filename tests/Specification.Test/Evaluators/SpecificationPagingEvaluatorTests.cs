using API;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationPagingEvaluatorTests
{
    [Fact]
    public void Paging_ShouldApplySkipOnly()
    {
        // Arrange
        var specification = new Specification<TestEntity> { Skip = 2 };
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(2, pagedResult.Count);
        Assert.Equal(3, pagedResult[0].Id);
        Assert.Equal(4, pagedResult[1].Id);
    }

    [Fact]
    public void Paging_ShouldApplyTakeOnly()
    {
        // Arrange
        var specification = new Specification<TestEntity> { Take = 2 };
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(2, pagedResult.Count);
        Assert.Equal(1, pagedResult[0].Id);
        Assert.Equal(2, pagedResult[1].Id);
    }

    [Fact]
    public void Paging_ShouldApplySkipAndTake()
    {
        // Arrange
        var specification = new Specification<TestEntity> { Skip = 1, Take = 2 };
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(2, pagedResult.Count);
        Assert.Equal(2, pagedResult[0].Id);
        Assert.Equal(3, pagedResult[1].Id);
    }

    [Fact]
    public void Paging_ShouldNotApplySkipOrTake_WhenNotSet()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(4, pagedResult.Count);
        Assert.Equal(1, pagedResult[0].Id);
        Assert.Equal(2, pagedResult[1].Id);
        Assert.Equal(3, pagedResult[2].Id);
        Assert.Equal(4, pagedResult[3].Id);
    }

    [Fact]
    public void Paging_ShouldApplySkipOnly_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto> { Skip = 2 };
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(2, pagedResult.Count);
        Assert.Equal(3, pagedResult[0].Id);
        Assert.Equal(4, pagedResult[1].Id);
    }

    [Fact]
    public void Paging_ShouldApplyTakeOnly_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto> { Take = 2 };
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(2, pagedResult.Count);
        Assert.Equal(1, pagedResult[0].Id);
        Assert.Equal(2, pagedResult[1].Id);
    }

    [Fact]
    public void Paging_ShouldApplySkipAndTake_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto> { Skip = 1, Take = 2 };
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(2, pagedResult.Count);
        Assert.Equal(2, pagedResult[0].Id);
        Assert.Equal(3, pagedResult[1].Id);
    }

    [Fact]
    public void Paging_ShouldNotApplySkipOrTake_ForEntityResultSpecification_WhenNotSet()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();
        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
            new() { Id = 4, Name = "Entity4" }
        }.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.Paging(specification);

        // Assert
        var pagedResult = result.ToList();
        Assert.Equal(4, pagedResult.Count);
        Assert.Equal(1, pagedResult[0].Id);
        Assert.Equal(2, pagedResult[1].Id);
        Assert.Equal(3, pagedResult[2].Id);
        Assert.Equal(4, pagedResult[3].Id);
    }
}
