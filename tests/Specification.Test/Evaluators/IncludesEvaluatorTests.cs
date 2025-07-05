using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class IncludesEvaluatorTests
{
    public class Parent
    {
        public int Id { get; set; }
        public List<Child> Children { get; set; } = new();
    }

    public class Child
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class TestDbContext : DbContext
    {
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    }

    [Fact]
    public void Evaluate_AppliesIncludes_OnSpecification()
    {
        // Setup InMemory DbContext
        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("IncludesEvaluatorTestDb")
            .Options;

        using var context = new TestDbContext(options);
        // Seed data
        var parent = new Parent { Id = 1, Children = [new Child { Id = 2, Value = "child1" }] };
        context.Parents.Add(parent);
        context.SaveChanges();

        IQueryable<Parent> query = context.Parents.AsQueryable();

        // Setup mock ISpecification with one IncludeExpression for Children
        var includeExpressions = new List<IncludeExpression>
        {
            new(
                (Expression<Func<Parent, IEnumerable<Child>>>)(p => p.Children))
        };

        var mockSpec = new Mock<ISpecification<Parent>>();
        mockSpec.Setup(s => s.IncludeExpressions).Returns(includeExpressions);

        IncludesEvaluator evaluator = IncludesEvaluator.Instance;

        // Act
        var result = evaluator.Evaluate(query, mockSpec.Object).ToList();

        // Assert
        Assert.Single(result);
        Assert.NotNull(result[0].Children);
        Assert.Single(result[0].Children);
        Assert.Equal("child1", result[0].Children.First().Value);
    }
}
