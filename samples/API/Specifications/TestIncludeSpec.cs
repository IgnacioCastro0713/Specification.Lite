
using Specification.Lite;

namespace API.Specifications;

public class TestIncludeSpec : Specification.Lite.Specification<TestEntityWithRelation>
{
    public TestIncludeSpec() =>
        Query
            .Include(e => e.Related)
            .ThenInclude(e => e!.Nested)
            .ThenInclude(e => e!.Deeps)
            .ThenInclude(e => e.MoreDeep);
}
