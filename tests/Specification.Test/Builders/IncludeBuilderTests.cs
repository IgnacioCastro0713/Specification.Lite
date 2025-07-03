using System.Linq.Expressions;
using API;
using Specification.Lite;
using Specification.Lite.Builders;
using Specification.Lite.Common;
using Specification.Lite.Expressions;

namespace Specification.Test.Builders;

public class IncludeBuilderTests
{
    [Fact]
    public void Include_ShouldAddIncludeExpression()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var builder = new IncludeBuilder<TestEntity, string>(specification);
        Expression<Func<TestEntity, string>> includeExpression = e => e.Name;

        // Act
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(includeExpression));

        // Assert
        Assert.Single(specification.IncludeExpressions);
        Assert.Equal(includeExpression, specification.IncludeExpressions.First().LambdaExpression);
        Assert.Equal(IncludeType.Include, specification.IncludeExpressions.First().Type);
    }

    [Fact]
    public void ThenInclude_ShouldAddThenIncludeExpression()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var builder = new IncludeBuilder<TestEntity, TestEntityWithRelation>(specification);
        Expression<Func<TestEntityWithRelation, TestRelatedEntity>> thenIncludeExpression = e => e.Related!;

        // Act
        builder.Specification.IncludeExpressions.Add(new IncludeExpression(thenIncludeExpression, typeof(TestEntityWithRelation)));

        // Assert
        Assert.Single(specification.IncludeExpressions);
        Assert.Equal(thenIncludeExpression, specification.IncludeExpressions.First().LambdaExpression);
        Assert.Equal(typeof(TestEntityWithRelation), specification.IncludeExpressions.First().PreviousPropertyType);
        Assert.Equal(IncludeType.ThenInclude, specification.IncludeExpressions.First().Type);
    }
}
