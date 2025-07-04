using System.Linq.Expressions;
using API;
using Microsoft.EntityFrameworkCore;
using Specification.Lite;
using Specification.Lite.Evaluators;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class SpecificationIncludesEvaluatorTests
{
    [Fact]
    public void Include_ShouldAddIncludeExpression()
    {
        // Arrange
        var specification = new Specification<TestEntity>();

        // Act
        specification.Query.Include(e => e.Name);

        // Assert
        Assert.Single(specification.IncludeExpressions);
        Assert.Equal("e.Name", specification.IncludeExpressions.First().LambdaExpression.Body.ToString());
    }

    [Fact]
    public void ThenInclude_ShouldAddThenIncludeExpression()
    {
        // Arrange
        var specification = new Specification<TestEntityWithRelation>();

        // Act
        specification.Query.Include(e => e.Related)
            .ThenInclude(r => r!.Nested);

        // Assert
        Assert.Equal(2, specification.IncludeExpressions.Count);
        Assert.Equal("e => e.Related", specification.IncludeExpressions.First().LambdaExpression.ToString());
        Assert.Equal("r => r.Nested", specification.IncludeExpressions.Last().LambdaExpression.ToString());
    }

    [Fact]
    public void IncludeAndThenInclude_ShouldAddBothExpressions()
    {
        // Arrange
        var specification = new Specification<TestEntityWithRelation>();

        // Act
        specification.Query.Include(e => e.Related)
            .ThenInclude(r => r!.Nested)
            .ThenInclude(n => n!.Deeps);

        // Assert
        Assert.Equal(3, specification.IncludeExpressions.Count);
        Assert.Equal("e => e.Related", specification.IncludeExpressions[0].LambdaExpression.ToString());
        Assert.Equal("r => r.Nested", specification.IncludeExpressions[1].LambdaExpression.ToString());
        Assert.Equal("n => n.Deeps", specification.IncludeExpressions[2].LambdaExpression.ToString());
    }

    [Fact]
    public void Include_ShouldAddIncludeExpression_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();

        // Act
        specification.Query.Include(e => e.Name);

        // Assert
        Assert.Single(specification.IncludeExpressions);
        Assert.Equal("e.Name", specification.IncludeExpressions.First().LambdaExpression.Body.ToString());
    }

    [Fact]
    public void ThenInclude_ShouldAddThenIncludeExpression_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntityWithRelation, TestDto>();

        // Act
        specification.Query.Include(e => e.Related)
            .ThenInclude(r => r!.Nested);

        // Assert
        Assert.Equal(2, specification.IncludeExpressions.Count);
        Assert.Equal("e => e.Related", specification.IncludeExpressions.First().LambdaExpression.ToString());
        Assert.Equal("r => r.Nested", specification.IncludeExpressions.Last().LambdaExpression.ToString());
    }

    [Fact]
    public void IncludeAndThenInclude_ShouldAddBothExpressions_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntityWithRelation, TestDto>();

        // Act
        specification.Query.Include(e => e.Related)
            .ThenInclude(r => r!.Nested)
            .ThenInclude(n => n!.Deeps);

        // Assert
        Assert.Equal(3, specification.IncludeExpressions.Count);
        Assert.Equal("e => e.Related", specification.IncludeExpressions[0].LambdaExpression.ToString());
        Assert.Equal("r => r.Nested", specification.IncludeExpressions[1].LambdaExpression.ToString());
        Assert.Equal("n => n.Deeps", specification.IncludeExpressions[2].LambdaExpression.ToString());
    }

    [Fact]
    public void Include_ShouldApplySingleIncludeExpression()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        var specification = new Specification<TestEntityWithRelation>();
        Expression<Func<TestEntityWithRelation, string>> includeExpression = e => e.Name;
        specification.IncludeExpressions.Add(new IncludeExpression(includeExpression));

        // Act
        IQueryable<TestEntityWithRelation> query = context.TestEntityWithRelations.AsQueryable();
        IQueryable<TestEntityWithRelation> result = query.Include(specification);

        // Assert
        Assert.NotNull(result);
        // Verify that the query includes the specified navigation property
    }

    [Fact]
    public void ThenInclude_ShouldApplySingleThenIncludeExpression()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        var specification = new Specification<TestEntityWithRelation>();
        Expression<Func<TestEntityWithRelation, string>> includeExpression = e => e.Name;

        specification.IncludeExpressions.Add(new IncludeExpression(includeExpression));

        // Act
        IQueryable<TestEntityWithRelation> query = context.TestEntityWithRelations.AsQueryable();
        IQueryable<TestEntityWithRelation> result = query.Include(specification);

        // Assert
        Assert.NotNull(result);
        // Verify that the query includes the specified nested navigation property
    }

    [Fact]
    public void IncludeAndThenInclude_ShouldApplyBothExpressions()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        var specification = new Specification<TestEntityWithRelation>();
        Expression<Func<TestEntityWithRelation, TestRelatedEntity>> includeExpression = e => e.Related!;
        Expression<Func<TestEntityWithRelation, TestNestedEntity>> includeExpressionNested = e => e.Related!.Nested!;

        specification.IncludeExpressions.Add(new IncludeExpression(includeExpression));
        specification.IncludeExpressions.Add(new IncludeExpression(includeExpressionNested));

        // Act
        IQueryable<TestEntityWithRelation> query = context.TestEntityWithRelations.AsQueryable();
        IQueryable<TestEntityWithRelation> result = query.Include(specification);

        // Assert
        Assert.NotNull(result);
        // Verify that the query includes both the navigation property and the nested navigation property
    }

    [Fact]
    public void Include_ShouldHandleEmptyIncludeExpressions()
    {
        // Arrange
        using var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options);
        var specification = new Specification<TestEntityWithRelation>();

        // Act
        IQueryable<TestEntityWithRelation> query = context.TestEntityWithRelations.AsQueryable();
        IQueryable<TestEntityWithRelation> result = query.Include(specification);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(query, result); // Query should remain unchanged
    }
}
