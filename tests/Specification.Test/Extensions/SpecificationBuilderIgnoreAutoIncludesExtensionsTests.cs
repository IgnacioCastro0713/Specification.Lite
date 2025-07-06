using API;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class SpecificationBuilderIgnoreAutoIncludesExtensionsTests
{
    [Fact]
    public void IgnoreAutoIncludes_ShouldApply_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specification.Query.IgnoreAutoIncludes();
        specificationResult.Query.IgnoreAutoIncludes();

        // Assert
        Assert.True(specification.IgnoreAutoIncludes);
        Assert.True(specificationResult.IgnoreAutoIncludes);
    }

    [Fact]
    public void IgnoreAutoIncludes_ShouldNotApply_WhenDisabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act & Assert
        Assert.False(specification.IgnoreAutoIncludes);
        Assert.False(specificationResult.IgnoreAutoIncludes);
    }
}
