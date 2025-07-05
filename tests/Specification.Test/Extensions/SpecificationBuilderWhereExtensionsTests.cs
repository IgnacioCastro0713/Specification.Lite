using System.Linq.Expressions;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class SpecificationBuilderWhereExtensionsTests
{
    [Fact]
    public void Where_Should_AddPredicate()
    {
        var builder = new Specification<int>();
        Expression<Func<int, bool>> predicate = x => x > 0;

        builder.Query.Where(predicate);

        Assert.Contains(builder.WhereExpressions, p => p == predicate);
    }

    [Fact]
    public void Where_MultiplePredicates_AreAllAdded()
    {
        var builder = new Specification<int>();
        Expression<System.Func<int, bool>> pred1 = x => x > 0;
        Expression<System.Func<int, bool>> pred2 = x => x < 10;

        builder.Query.Where(pred1).Where(pred2);

        Assert.Contains(builder.WhereExpressions, p => p == pred1);
        Assert.Contains(builder.WhereExpressions, p => p == pred2);
        Assert.Equal(2, builder.WhereExpressions.Count);
    }
}
