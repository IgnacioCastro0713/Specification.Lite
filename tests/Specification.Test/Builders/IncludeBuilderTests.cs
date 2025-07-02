using System.Linq.Expressions;
using API;
using Specification.Lite.Builders;
using Specification.Lite.Expressions;
using Specification.Lite.Extensions;

namespace Specification.Test.Builders;


public class IncludeBuilderTests
{
    [Fact]
    public void ThenInclude_AddsThenIncludeExpression()
    {
        // Arrange
        var includeExpression = new IncludeExpression<TestEntityWithRelation>((Expression<Func<TestEntityWithRelation, TestRelatedEntity>>)(e => e.Related!));
        var builder = new IncludeBuilder<TestEntityWithRelation, TestRelatedEntity>(includeExpression);

        Expression<Func<TestRelatedEntity, TestNestedEntity>> thenIncludeExpression = r => r.Nested!;

        // Act
        IncludeBuilder<TestEntityWithRelation, TestNestedEntity> resultBuilder = builder.ThenInclude(thenIncludeExpression);

        // Assert
        Assert.NotNull(resultBuilder);
        Assert.Single(includeExpression.ThenIncludes);
        Assert.Equal(thenIncludeExpression, includeExpression.ThenIncludes[0]);
    }

    [Fact]
    public void ThenInclude_WithEnumerable_AddsThenIncludeExpression()
    {
        // Arrange
        var includeExpression = new IncludeExpression<TestEntityWithRelation>((Expression<Func<TestEntityWithRelation, TestRelatedEntity>>)(e => e.Related!));
        var builder = new IncludeBuilder<TestEntityWithRelation, TestRelatedEntity>(includeExpression);

        Expression<Func<TestRelatedEntity, List<TestDeepEntity>>> thenIncludeExpression = r => r.Nested!.Deeps;

        // Act
        IncludeBuilder<TestEntityWithRelation, List<TestDeepEntity>> resultBuilder = builder.ThenInclude(thenIncludeExpression);

        // Assert
        Assert.NotNull(resultBuilder);
        Assert.Single(includeExpression.ThenIncludes);
        Assert.Equal(thenIncludeExpression, includeExpression.ThenIncludes[0]);
    }
}
