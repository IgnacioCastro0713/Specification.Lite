using Microsoft.EntityFrameworkCore;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class AsSplitQueryEvaluatorTests
{
    public sealed class TestEntity { public int Id { get; set; } }

    public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
    {
        public DbSet<TestEntity> Entities { get; set; }
    }

    [Fact]
    public void Evaluate_AppliesAsSplitQuery_WhenSpecificationRequestsIt()
    {
        // Arrange
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("SplitQueryEvaluatorTestDb1")
            .Options;

        using var context = new TestDbContext(options);
        context.Entities.AddRange(new TestEntity { Id = 1 }, new TestEntity { Id = 2 });
        context.SaveChanges();

        IQueryable<TestEntity> query = context.Entities.AsQueryable();

        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsSplitQuery).Returns(true);

        AsSplitQueryEvaluator evaluator = AsSplitQueryEvaluator.Instance;

        // Act
        var result = evaluator.Query(query, mockSpec.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Evaluate_DoesNotApplyAsSplitQuery_WhenNotRequested()
    {
        // Arrange
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("SplitQueryEvaluatorTestDb2")
            .Options;

        using var context = new TestDbContext(options);
        context.Entities.AddRange(new TestEntity { Id = 1 }, new TestEntity { Id = 2 });
        context.SaveChanges();

        IQueryable<TestEntity> query = context.Entities.AsQueryable();

        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsSplitQuery).Returns(false);

        AsSplitQueryEvaluator evaluator = AsSplitQueryEvaluator.Instance;

        // Act
        var result = evaluator.Query(query, mockSpec.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count);
    }
}
