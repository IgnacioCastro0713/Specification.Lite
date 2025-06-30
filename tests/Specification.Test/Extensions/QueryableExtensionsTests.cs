using Microsoft.EntityFrameworkCore;
using Specification.Lite;
using Specification.Lite.Extensions;

namespace Specification.Test.Extensions;

public class QueryableExtensionsTests
{
    public sealed class TestSpecification : Specification<TestEntity>;

    public sealed class TestProjectionSpecification : Specification<TestEntity, TestDto>
    {
        public TestProjectionSpecification() => Select(e => new TestDto { Id = e.Id, Name = e.Name });
    }

    [Fact]
    public async Task ToListAsync_WithSpecification_CallsSpecifyQuery()
    {
        // Arrange
        var specification = new TestSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Test1" },
            new TestEntity { Id = 2, Name = "Test2" },
            new TestEntity { Id = 3, Name = "Test3" }
        );
        await context.SaveChangesAsync();

        // Act
        List<TestEntity> expect = await context.TestEntities
            .ToListAsync(CancellationToken.None);

        List<TestEntity> result = await context.TestEntities
            .ToListAsync(specification, CancellationToken.None);

        // Assert 2/2
        Assert.Equal(expect.Count, result.Count);
    }

    [Fact]
    public async Task ToListAsync_WithProjectionSpecification_ReturnsProjectedList()
    {
        // Arrange
        var specification = new TestProjectionSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Test1" },
            new TestEntity { Id = 2, Name = "Test2" }
        );
        await context.SaveChangesAsync();

        // Act
        List<TestDto> result = await context.TestEntities
            .ToListAsync(specification, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Test1", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Test2", result[1].Name);
    }

    [Fact]
    public async Task FirstOrDefaultAsync_WithSpecification_GetsFirstOrDefaultResult()
    {
        // Arrange
        var specification = new TestSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Test1" },
            new TestEntity { Id = 2, Name = "Test2" }
        );
        await context.SaveChangesAsync();

        // Act
        TestEntity? firstEntity = await context.TestEntities
            .FirstOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(firstEntity);
        Assert.Equal(1, firstEntity.Id);
    }

    [Fact]
    public async Task FirstOrDefaultAsync_WithProjectionSpecification_ReturnsProjectedFirstOrDefault()
    {
        // Arrange
        var specification = new TestProjectionSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Test1" },
            new TestEntity { Id = 2, Name = "Test2" }
        );
        await context.SaveChangesAsync();

        // Act
        TestDto? firstDto = await context.TestEntities
            .FirstOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(firstDto);
        Assert.Equal(1, firstDto.Id);
        Assert.Equal("Test1", firstDto.Name);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_WithSpecification_GetsSingleOrDefaultResult()
    {
        // Arrange
        var specification = new TestSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.Add(new TestEntity { Id = 1, Name = "Test1" });
        await context.SaveChangesAsync();

        // Act
        TestEntity? singleEntity = await context.TestEntities
            .SingleOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(singleEntity);
        Assert.Equal(1, singleEntity.Id);
    }

    [Fact]
    public async Task SingleOrDefaultAsync_WithProjectionSpecification_ReturnsProjectedSingleOrDefault()
    {
        // Arrange
        var specification = new TestProjectionSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.Add(new TestEntity { Id = 1, Name = "Test1" });
        await context.SaveChangesAsync();

        // Act
        TestDto? singleDto = await context.TestEntities
            .SingleOrDefaultAsync(specification);

        // Assert
        Assert.NotNull(singleDto);
        Assert.Equal(1, singleDto.Id);
        Assert.Equal("Test1", singleDto.Name);
    }

    [Fact]
    public async Task AnyAsync_WithSpecification_ReturnsTrueWhenElementsExist()
    {
        // Arrange
        var specification = new TestSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.Add(new TestEntity { Id = 1, Name = "Test1" });
        await context.SaveChangesAsync();

        // Act
        bool hasAny = await context.TestEntities
            .AnyAsync(specification);

        // Assert
        Assert.True(hasAny);
    }

    [Fact]
    public async Task AnyAsync_WithSpecification_ReturnsFalseWhenNoElementsExist()
    {
        // Arrange
        var specification = new TestSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        // Don't add any entities
        await context.SaveChangesAsync();

        // Act
        bool hasAny = await context.TestEntities
            .AnyAsync(specification);

        // Assert
        Assert.False(hasAny);
    }

    [Fact]
    public async Task AnyAsync_WithProjectionSpecification_ReturnsTrueWhenElementsExist()
    {
        // Arrange
        var specification = new TestProjectionSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.Add(new TestEntity { Id = 1, Name = "Test1" });
        await context.SaveChangesAsync();

        // Act
        bool hasAny = await context.TestEntities
            .AnyAsync(specification);

        // Assert
        Assert.True(hasAny);
    }

    [Fact]
    public async Task AnyAsync_WithProjectionSpecification_ReturnsFalseWhenNoElementsExist()
    {
        // Arrange
        var specification = new TestProjectionSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        // Don't add any entities
        await context.SaveChangesAsync();

        // Act
        bool hasAny = await context.TestEntities
            .AnyAsync(specification);

        // Assert
        Assert.False(hasAny);
    }
}