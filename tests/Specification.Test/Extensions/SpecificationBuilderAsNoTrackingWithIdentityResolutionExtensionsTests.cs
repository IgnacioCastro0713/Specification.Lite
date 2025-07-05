using API;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class SpecificationBuilderAsNoTrackingWithIdentityResolutionExtensionsTests
{
    [Fact]
    public void AsNoTrackingWithIdentityResolution_ShouldApplyAsAsNoTrackingWithIdentityResolution_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specification.Query.AsNoTrackingWithIdentityResolution();
        specificationResult.Query.AsNoTrackingWithIdentityResolution();

        // Assert
        Assert.True(specification.AsNoTrackingWithIdentityResolution);
        Assert.True(specificationResult.AsNoTrackingWithIdentityResolution);
    }

    [Fact]
    public void AsNoTrackingWithIdentityResolution_ShouldNotApplyAsNoTrackingWithIdentityResolution_WhenDisabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act & Assert
        Assert.False(specification.AsNoTrackingWithIdentityResolution);
        Assert.False(specificationResult.AsNoTrackingWithIdentityResolution);
    }

    [Fact]
    public void AsNoTrackingWithIdentityResolution_ShouldNotAffectOtherProperties_ForEntityResultSpecification()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specification.Query.AsNoTrackingWithIdentityResolution();
        specificationResult.Query.AsNoTrackingWithIdentityResolution();

        // Assert
        Assert.True(specification.AsNoTrackingWithIdentityResolution);
        Assert.False(specification.AsNoTracking);
        Assert.False(specification.AsTracking);

        Assert.True(specificationResult.AsNoTrackingWithIdentityResolution);
        Assert.False(specificationResult.AsNoTracking);
        Assert.False(specificationResult.AsTracking);
    }
}
