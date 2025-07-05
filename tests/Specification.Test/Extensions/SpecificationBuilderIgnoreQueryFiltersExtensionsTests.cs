using API;
using Specification.Lite;

namespace Specification.Test.Extensions;
public class SpecificationBuilderIgnoreQueryFiltersExtensionsTests
{
    [Fact]
    public void IgnoreQueryFilters_ShouldApplyIgnoreQueryFilters_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act
        specification.Query.IgnoreQueryFilters();
        specificationResult.Query.IgnoreQueryFilters();


        // Assert
        Assert.True(specification.IgnoreQueryFilters);
        Assert.True(specificationResult.IgnoreQueryFilters);
    }

    [Fact]
    public void IgnoreQueryFilters_ShouldBeFalseByDefault()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act & Assert
        Assert.False(specification.IgnoreQueryFilters);
        Assert.False(specificationResult.IgnoreQueryFilters);
    }
}
