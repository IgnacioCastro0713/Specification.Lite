using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationDistinctnessEvaluatorTests
{
    public sealed class TestDistinctSpecification : Specification<TestEntity>
    {
        public TestDistinctSpecification() => Distinct();
    }




    [Fact]
    public void ApplyDistinctness_WithNoDistinctness_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.IsDistinct).Returns(false);
        mockSpecification.Setup(s => s.DistinctBySelector).Returns((Expression<Func<TestEntity, object>>?)null);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        IQueryable<TestEntity> result = query.ApplyDistinctness(mockSpecification.Object);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }




    [Fact]
    public async Task ApplyDistinctness_WithDistinct_RemovesDuplicates()
    {
        // Arrange
        var specification = new TestDistinctSpecification();

        // Use in-memory database
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"ExtensionsTestDb_{Guid.NewGuid()}")
            .Options;

        await using var context = new TestDbContext(options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Test1" },
            new TestEntity { Id = 2, Name = "Test1" },
            new TestEntity { Id = 3, Name = "Test3" }
        );
        await context.SaveChangesAsync();

        // Act
        List<TestEntity> result = await context.TestEntities.Select(b => new TestEntity { Name = b.Name }).ApplyDistinctness(specification).ToListAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void ApplyDistinctness_WithDistinctBy_RemovesDuplicatesBySelector()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.IsDistinct).Returns(false);
        mockSpecification.Setup(s => s.DistinctBySelector).Returns(e => e.Id);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 1, Name = "DifferentName" },
            new() { Id = 2, Name = "Test2" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.ApplyDistinctness(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        // The first entity with Id=1 should be kept
        Assert.Contains(result, e => e is { Id: 1, Name: "Test1" });
    }
}
