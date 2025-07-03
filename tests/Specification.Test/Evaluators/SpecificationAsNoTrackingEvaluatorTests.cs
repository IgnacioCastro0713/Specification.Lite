using API;
using Specification.Lite;

namespace Specification.Test.Evaluators;

public class SpecificationAsNoTrackingEvaluatorTests
{

    [Fact]
    public void AsNoTracking_ShouldApplyAsTracking_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act
        specification.Query.AsNoTracking();
        specificationResult.Query.AsNoTracking();


        // Assert
        Assert.True(specification.AsNoTracking);
        Assert.True(specificationResult.AsNoTracking);
    }

    [Fact]
    public void AsNoTracking_ShouldNotApplyAsTracking_WhenDisabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.AsNoTracking);
        Assert.False(specificationResult.AsNoTracking);
    }
}
