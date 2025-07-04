using API;
using Specification.Lite;
using Specification.Lite.Exceptions;

namespace Specification.Test.Evaluators;

public class SpecificationAsTrackingEvaluatorTests
{
    [Fact]
    public void AsTracking_ShouldApplyAsTracking_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act
        specification.Query.AsTracking();
        specificationResult.Query.AsTracking();

        // Assert
        Assert.True(specification.AsTracking);
        Assert.True(specificationResult.AsTracking);
    }

    [Fact]
    public void AsTracking_ShouldNotApplyAsTracking_WhenDisabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.AsTracking);
        Assert.False(specificationResult.AsTracking);
    }

    [Fact]
    public void AsTracking_ShouldThrowException_WhenAsNoTrackingIsEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query.AsNoTracking();

        // Act & Assert
        Assert.Throws<ConcurrentTrackingException>(() => specification.Query.AsTracking());
    }

    [Fact]
    public void AsTracking_ShouldBeFalseByDefault()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.AsTracking);
        Assert.False(specificationResult.AsTracking);
    }

    [Fact]
    public void AsTracking_ShouldNotOverwriteOtherModifiers_WhenCalledMultipleTimes()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        specification.Query
            .AsTracking()
            .Where(e => e.Id > 1)
            .AsTracking();

        // Act
        bool isTracking = specification.AsTracking;
        var whereExpressions = specification.WhereExpressions;

        // Assert
        Assert.True(isTracking);
        Assert.Single(whereExpressions);
        Assert.Equal("e => (e.Id > 1)", whereExpressions.First().ToString());
    }
}
