using API;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class SpecificationBuilderAsTrackingExtensionsTests
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
    public void AsTracking_ShouldNotAffectOtherProperties_ForEntityResultSpecification()
    {
        // Arrange
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specificationResult.Query.AsTracking();

        // Assert
        Assert.True(specificationResult.AsTracking);
        Assert.False(specificationResult.AsNoTracking);
        Assert.False(specificationResult.AsNoTrackingWithIdentityResolution);
    }
}
