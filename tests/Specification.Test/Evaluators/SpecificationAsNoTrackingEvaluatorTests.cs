using System.Linq.Expressions;
using API;
using Specification.Lite;
using Specification.Lite.Exceptions;
using Specification.Lite.Expressions;

namespace Specification.Test.Evaluators;

public class SpecificationAsNoTrackingEvaluatorTests
{
    [Fact]
    public void AsNoTracking_ShouldApplyAsNoTracking_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specification.Query.AsNoTracking();
        specificationResult.Query.AsNoTracking();

        // Assert
        Assert.True(specification.AsNoTracking);
        Assert.True(specificationResult.AsNoTracking);
    }

    [Fact]
    public void AsNoTracking_ShouldNotApplyAsNoTracking_WhenDisabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act & Assert
        Assert.False(specification.AsNoTracking);
        Assert.False(specificationResult.AsNoTracking);
    }

    [Fact]
    public void AsNoTracking_ShouldNotAffectOtherProperties_ForEntityResultSpecification()
    {
        // Arrange
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specificationResult.Query.AsNoTracking();

        // Assert
        Assert.True(specificationResult.AsNoTracking);
        Assert.False(specificationResult.AsTracking); // Ensure AsTracking is not enabled
        Assert.Equal(-1, specificationResult.Take); // Ensure Take is not affected
        Assert.Equal(-1, specificationResult.Skip); // Ensure Skip is not affected
    }

    [Fact]
    public void AsNoTracking_ShouldWorkWithOtherQueryModifiers_ForEntityResultSpecification()
    {
        // Arrange
        var specificationResult = new Specification<TestEntity, TestDto>();
        specificationResult.Query
            .AsNoTracking()
            .Where(e => e.Id > 1)
            .OrderBy(e => e.Name);

        // Act
        bool isNoTracking = specificationResult.AsNoTracking;
        List<Expression<Func<TestEntity, bool>>> whereExpressions = specificationResult.WhereExpressions;
        List<OrderExpression<TestEntity>> orderExpressions = specificationResult.OrderExpressions;

        // Assert
        Assert.True(isNoTracking);
        Assert.Single(whereExpressions);
        Assert.Single(orderExpressions);
        Assert.Equal("e => (e.Id > 1)", whereExpressions.First().ToString());
        Assert.Equal("e => e.Name", orderExpressions.First().KeySelector.ToString());
    }

    [Fact]
    public void AsNoTracking_ShouldThrowException_WhenAsTrackingIsEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.AsTracking();

        // Act & Assert
        Assert.Throws<ConcurrentTrackingException>(() => specification.Query.AsNoTracking());
    }

    [Fact]
    public void AsNoTracking_ShouldBeFalseByDefault()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act & Assert
        Assert.False(specification.AsNoTracking);
        Assert.False(specificationResult.AsNoTracking);
    }

    [Fact]
    public void AsNoTracking_ShouldNotOverwriteOtherModifiers_WhenCalledMultipleTimes()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query
            .AsNoTracking()
            .Where(e => e.Id > 1)
            .AsNoTracking();

        // Act
        bool isNoTracking = specification.AsNoTracking;
        List<Expression<Func<TestEntity, bool>>> whereExpressions = specification.WhereExpressions;

        // Assert
        Assert.True(isNoTracking);
        Assert.Single(whereExpressions);
        Assert.Equal("e => (e.Id > 1)", whereExpressions.First().ToString());
    }
}
