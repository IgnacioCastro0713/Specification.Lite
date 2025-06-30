using Specification.Lite;

namespace API.Specifications;

public class TestIncludeSpec : Specification<TestEntityWithRelation>
{
    public TestIncludeSpec()
    {
        Where(entity => entity.Id == 1);

        Include(e => e.Related)
            .ThenInclude(e => e!.Nested)
            .ThenInclude(e => e!.Deeps);
    }
}
