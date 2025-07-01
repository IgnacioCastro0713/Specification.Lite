using Specification.Lite;

namespace API.Specifications;

public class TestIncludeSpec : Specification<TestEntityWithRelation>
{
    public TestIncludeSpec() =>
        Include(e => e.Related)
            .ThenInclude(e => e!.Nested)
            .ThenInclude(e => e!.Deeps);
}
