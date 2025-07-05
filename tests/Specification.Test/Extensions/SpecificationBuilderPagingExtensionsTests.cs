using API;
using Specification.Lite;

namespace Specification.Test.Extensions;
public class SpecificationBuilderPagingExtensionsTests
{
    [Fact]
    public void ApplyPaging_Should_SetSkipAndTake()
    {
        var specification = new Specification<TestEntity>();

        specification.Query.Skip(10).Take(5);

        Assert.Equal(10, specification.Skip);
        Assert.Equal(5, specification.Take);
    }



    [Fact]
    public void ApplyPaging_NegativeValues_AreAccepted()
    {
        var specification = new Specification<TestEntity>();


        Assert.Equal(-1, specification.Skip);
        Assert.Equal(-1, specification.Take);
    }
}
