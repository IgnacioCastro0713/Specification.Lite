using System.Linq.Expressions;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class SpecificationBuilderSelectorExtensionsTests
{
    private sealed class Dummy { public int Value { get; set; } }

    [Fact]
    public void Select_Should_SetSelector()
    {
        var specification = new Specification<Dummy, int>();
        Expression<Func<Dummy, int>> selector = x => x.Value;

        specification.Query.Select(selector);

        Assert.Equal(selector, specification.Selector);
    }
}
