using API;
using Specification.Lite;

namespace Specification.Test.Evaluators;

public class SpecificationIgnoreQueryFiltersEvaluatorTests
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
    public void IgnoreQueryFilters_ShouldNotApplyIgnoreQueryFilters_WhenDisabled()
    {
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.IgnoreQueryFilters);
        Assert.False(specificationResult.IgnoreQueryFilters);
    }
}
