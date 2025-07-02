using Specification.Lite;
using Specification.Lite.Extensions;

namespace API.Specifications;

public class TestWhereSpec : Specification<TestEntity>
{
    public TestWhereSpec() =>
        Query
            .Where(entity => entity.Id == 4 || entity.Id == 1)
            .Where(entity => entity.Id == 4);
}
