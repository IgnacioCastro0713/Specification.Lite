using API;
using Specification.Lite;

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
}
