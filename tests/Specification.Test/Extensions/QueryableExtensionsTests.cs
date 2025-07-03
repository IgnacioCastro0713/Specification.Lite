using API;
using Microsoft.EntityFrameworkCore;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class QueryableExtensionsTests
{

    [Fact]
    public async Task ToListAsync_ShouldReturnEntitiesMatchingSpecification()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id > 1);

        // Act
        List<TestEntity> result = await context.TestEntities.ToListAsync(specification);

        // Assert
        Assert.Single(result);
        Assert.Equal("Entity2", result.First().Name);
    }

    [Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnFirstEntityMatchingSpecification()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id > 1);

        // Act
        TestEntity? result = await context.TestEntities.FirstOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity2", result!.Name);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_ShouldReturnSingleEntityMatchingSpecification()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id == 1);

        // Act
        TestEntity? result = await context.TestEntities.SingleOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity1", result!.Name);
    }

    [Fact]
    public async Task AnyAsync_ShouldReturnTrueIfEntitiesMatchSpecification()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id > 1);

        // Act
        bool result = await context.TestEntities.AnyAsync(specification);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AnyAsync_ShouldReturnFalseIfNoEntitiesMatchSpecification()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity>();
        specification.Query.Where(e => e.Id > 10);

        // Act
        bool result = await context.TestEntities.AnyAsync(specification);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ToListAsync_ShouldReturnResultsMatchingSpecification_WithSelector()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id > 1).Select(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act
        List<TestDto> result = await context.TestEntities.ToListAsync(specification);

        // Assert
        Assert.Single(result);
        Assert.Equal("Entity2", result.First().Name);
    }

    [Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnFirstResultMatchingSpecification_WithSelector()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id > 1).Select(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act
        TestDto? result = await context.TestEntities.FirstOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity2", result!.Name);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_ShouldReturnSingleResultMatchingSpecification_WithSelector()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id == 1).Select(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act
        TestDto? result = await context.TestEntities.SingleOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Entity1", result!.Name);
    }

    [Fact]
    public async Task AnyAsync_ShouldReturnTrueIfResultsMatchSpecification_WithSelector()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id > 1).Select(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act
        bool result = await context.TestEntities.AnyAsync(specification);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AnyAsync_ShouldReturnFalseIfNoResultsMatchSpecification_WithSelector()
    {
        // Arrange
        await using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        await context.SaveChangesAsync();

        var specification = new Specification<TestEntity, TestDto>();
        specification.Query.Where(e => e.Id > 10).Select(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act
        bool result = await context.TestEntities.AnyAsync(specification);

        // Assert
        Assert.False(result);
    }
}
