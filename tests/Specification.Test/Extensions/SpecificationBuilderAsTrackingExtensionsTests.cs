using API;
using Specification.Lite;
using Specification.Lite.Exceptions;

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
}
