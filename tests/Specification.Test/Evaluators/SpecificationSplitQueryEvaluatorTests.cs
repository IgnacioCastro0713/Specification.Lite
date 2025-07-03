using API;
using Specification.Lite;

namespace Specification.Test.Evaluators;

public class SpecificationSplitQueryEvaluatorTests
{
    [Fact]
    public void SplitQuery_ShouldApplySplitQuery_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act
        specification.Query.SplitQuery();
        specificationResult.Query.SplitQuery();


        // Assert
        Assert.True(specification.AsSplitQuery);
        Assert.True(specificationResult.AsSplitQuery);
    }

    [Fact]
    public void SplitQuery_ShouldNotApplySplitQuery_WhenDisabled()
    {
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, string>();

        // Act & Assert
        Assert.False(specification.AsNoTracking);
        Assert.False(specificationResult.AsSplitQuery);
    }
}
