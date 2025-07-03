using API;
using Microsoft.EntityFrameworkCore;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationEvaluatorTests
{
    [Fact]
    public void WithSpecification_ShouldApplyAllEvaluators()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}").Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" },
            new TestEntity { Id = 3, Name = "Entity3" }
        );
        context.SaveChanges();

        var specification = new Specification<TestEntity>();
        specification.Query
            .Where(e => e.Id > 1)
            .OrderBy(e => e.Name)
            .Take(1)
            .AsNoTracking();

        // Act
        var result = context.TestEntities.WithSpecification(specification).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Entity2", result.First().Name);
    }

    [Fact]
    public void WithSpecification_ShouldApplySelectors_ForEntityResultSpecification()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}").Options);
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
        );
        context.SaveChanges();

        var specification = new Specification<TestEntity, TestDto>
        {
            Selector = e => new TestDto { Id = e.Id, Name = e.Name }
        };
        specification.Query.Where(e => e.Id > 1);

        // Act
        var result = context.TestEntities.WithSpecification(specification).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Entity2", result.First().Name);
    }

    [Fact]
    public void WithSpecification_ShouldApplyManySelectors_ForEntityResultSpecification()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}").Options);
        context.TestEntityWithRelations.Add(new TestEntityWithRelation
        {
            Id = 1,
            Name = "EntityWithRelation",
            Related = new TestRelatedEntity
            {
                Id = 1,
                Department = "HR",
                Nested = new TestNestedEntity
                {
                    Id = 1,
                    Value = "NestedValue"
                }
            }
        });
        context.SaveChanges();

        var specification = new Specification<TestEntityWithRelation, TestDto>
        {
            ManySelector = e => e.Related!.Nested!.Deeps.Select(d => new TestDto { Id = d.Id, Name = d.Value })
        };

        // Act
        var result = context.TestEntityWithRelations.WithSpecification(specification).ToList();

        // Assert
        Assert.Empty(result); // No Deeps were seeded, so the result should be empty
    }
}
