using System.Linq.Expressions;
using API;
using Specification.Lite;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class SpecificationSplitQueryEvaluatorTests
{
    [Fact]
    public void SplitQuery_ShouldApplySplitQuery_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act
        specification.Query.SplitQuery();
        specificationResult.Query.SplitQuery();


        // Assert
        Assert.True(specification.AsSplitQuery);
        Assert.True(specificationResult.AsSplitQuery);
    }

    [Fact]
    public void SplitQuery_ShouldNotApplySplitQuery_WhenDisabled()
    {
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.AsNoTracking);
        Assert.False(specificationResult.AsSplitQuery);
    }

    [Fact]
    public void SplitQuery_ShouldNotAffectOtherQueryModifiers()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query
            .SplitQuery()
            .Where(e => e.Id > 1)
            .OrderBy(e => e.Name);

        // Act
        bool isSplitQuery = specification.AsSplitQuery;
        List<Expression<Func<TestEntity, bool>>> whereExpressions = specification.WhereExpressions;
        List<OrderExpression<TestEntity>> orderExpressions = specification.OrderExpressions;

        // Assert
        Assert.True(isSplitQuery);
        Assert.Single(whereExpressions);
        Assert.Single(orderExpressions);
        Assert.Equal("e => (e.Id > 1)", whereExpressions.First().ToString());
        Assert.Equal("e => e.Name", orderExpressions.First().KeySelector.ToString());
    }

    [Fact]
    public void SplitQuery_ShouldWorkWithEntityResultSpecification()
    {
        // Arrange
        var specificationResult = new Specification<TestEntity, TestDto>();
        specificationResult.Query
            .SplitQuery()
            .Where(e => e.Id > 1)
            .Select(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act
        bool isSplitQuery = specificationResult.AsSplitQuery;
        List<Expression<Func<TestEntity, bool>>> whereExpressions = specificationResult.WhereExpressions;
        Expression<Func<TestEntity, TestDto>>? selector = specificationResult.Selector;

        // Assert
        Assert.True(isSplitQuery);
        Assert.Single(whereExpressions);
        Assert.NotNull(selector);
        Assert.Equal("e => (e.Id > 1)", whereExpressions.First().ToString());
    }

    [Fact]
    public void SplitQuery_ShouldNotOverwriteOtherModifiers_WhenCalledMultipleTimes()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query
            .SplitQuery()
            .Where(e => e.Id > 1)
            .SplitQuery();

        // Act
        bool isSplitQuery = specification.AsSplitQuery;
        List<Expression<Func<TestEntity, bool>>> whereExpressions = specification.WhereExpressions;

        // Assert
        Assert.True(isSplitQuery);
        Assert.Single(whereExpressions);
        Assert.Equal("e => (e.Id > 1)", whereExpressions.First().ToString());
    }
}
