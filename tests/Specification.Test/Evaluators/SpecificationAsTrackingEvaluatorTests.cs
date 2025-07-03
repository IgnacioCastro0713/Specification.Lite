using API;
using Specification.Lite;

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
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.AsTracking);
        Assert.False(specificationResult.AsTracking);
    }
}
